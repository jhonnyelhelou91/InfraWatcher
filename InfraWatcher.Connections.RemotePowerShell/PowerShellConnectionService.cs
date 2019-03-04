using InfraWatcher.Core.Services;
using System;
using InfraWatcher.Core.Models.Command;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Exceptions;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using InfraWatcher.Core.Extensions;
using System.Threading;
using System.Diagnostics;
using System.Linq;

namespace InfraWatcher.Connections.RemotePowerShell
{
    public class PowerShellConnectionService : IServerConnectionService, IDisposable
    {
        Runspace RunspaceInstance;

        public ServerConnection ServerConnection { get; private set; }
        public bool IsConnected { get; private set; }
        public DateTime? ConnectionDate { get; private set; }
        public TimeSpan? ConnectionDuration { get; private set; }

        public bool Connect(ServerConnection serverConnection)
        {
            if (serverConnection == null)
                throw new ArgumentNullException(nameof(serverConnection));

            if (serverConnection.Credential == null)
                throw new ArgumentNullException(nameof(serverConnection.Credential));

            if (string.IsNullOrEmpty(serverConnection.Host))
                throw new ArgumentNullException(nameof(serverConnection.Host));

            if (string.IsNullOrEmpty(serverConnection.Credential.Username))
                throw new ArgumentNullException(nameof(serverConnection.Credential.Username));

            serverConnection.Port = serverConnection.Port ?? 3389;

            ServerConnection = serverConnection;


            try
            {

                if (serverConnection.Host != Environment.MachineName)
                {
                    var powershellCredential =
                        new PSCredential(serverConnection.Credential.Username,
                            ServerConnection.Credential.Password?.ToSecureString());

                    WSManConnectionInfo connectionInfo =
                        new WSManConnectionInfo(WSManConnectionInfo.HttpsScheme,
                            serverConnection.Host,
                            serverConnection.Port.Value, "/wsman",
                            "http://schemas.microsoft.com/powershell/Microsoft.PowerShell",
                            powershellCredential);

                    if (ServerConnection.Timeout.HasValue)
                    {
                        connectionInfo.CancelTimeout = (int)ServerConnection.Timeout.Value.TotalMilliseconds;
                        connectionInfo.OpenTimeout = (int)ServerConnection.Timeout.Value.TotalMilliseconds;
                        connectionInfo.OperationTimeout = (int)ServerConnection.Timeout.Value.TotalMilliseconds;
                    }

                    var now = DateTime.UtcNow;
                    RunspaceInstance = RunspaceFactory.CreateRunspace(connectionInfo);
                    RunspaceInstance.Open();

                    ConnectionDate = DateTime.UtcNow;
                    IsConnected = true;
                    ConnectionDuration = ConnectionDate.Value.Subtract(now);
                }
                else
                {
                    var now = DateTime.UtcNow;
                    RunspaceInstance = RunspaceFactory.CreateRunspace();
                    RunspaceInstance.Open();

                    ConnectionDate = DateTime.UtcNow;
                    IsConnected = true;
                    ConnectionDuration = ConnectionDate.Value.Subtract(now);
                }
            }
            catch (Exception exception)
            {
                throw new ServerConnectionException(exception.Message);
            }

            return IsConnected;
        }

        public void Disconnect()
        {
            if (RunspaceInstance != null)
            {
                RunspaceInstance.Close();
                RunspaceInstance.Disconnect();
                RunspaceInstance.Dispose();
                RunspaceInstance = null;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        public ServerCommandResult ExecuteCommand(ServerCommand serverCommand)
        {
            if (!IsConnected)
            {
                throw new ServerConnectionException("Server connection is not established");
            }
            if (serverCommand == null)
                throw new ArgumentNullException(nameof(serverCommand));
            if (string.IsNullOrEmpty(serverCommand.Text))
                throw new CommandNotSpecifiedException($"Command was not defined");

            serverCommand.Timeout = serverCommand.Timeout ?? TimeSpan.FromMinutes(1);

            var result = new ServerCommandResult()
            {
                Result = "",
                Error = ""
            };

            using (var powershell = PowerShell.Create())
            {
                powershell.Runspace = RunspaceInstance;
                powershell.AddScript(serverCommand.Text);

                PSDataCollection<PSObject> output = new PSDataCollection<PSObject>();
                #region Subscribe to streams
                powershell.Streams.Debug.DataAdded +=
                            (sender, e) => result.Result += Debug_DataAdded(sender, e);
                powershell.Streams.Verbose.DataAdded +=
                    (sender, e) => result.Result += Verbose_DataAdded(sender, e);
                powershell.Streams.Progress.DataAdded +=
                    (sender, e) => result.Result += Progress_DataAdded(sender, e);
                powershell.Streams.Warning.DataAdded +=
                    (sender, e) => result.Result += Warning_DataAdded(sender, e);
                powershell.Streams.Error.DataAdded +=
                    (sender, e) => result.Error += Error_DataAdded(sender, e);
                #endregion

                IAsyncResult powershellResult = powershell.BeginInvoke();
                Stopwatch stopWatch = Stopwatch.StartNew();

                while (!powershellResult.IsCompleted)
                {
                    Thread.Sleep(500);
                    if (stopWatch.Elapsed > serverCommand.Timeout)
                    {
                        if (stopWatch.IsRunning)
                            stopWatch.Stop();
                        if (powershell.InvocationStateInfo.State == PSInvocationState.Running)
                            powershell.Stop();
                        powershell.Dispose();
                        Dispose();

                        throw new CommandTimeoutException($"Command exceeded timeout {serverCommand.Timeout}");
                    }
                }

                if (stopWatch.IsRunning)
                    stopWatch.Stop();
                if (powershell.InvocationStateInfo.State == PSInvocationState.Running)
                    powershell.EndInvoke(powershellResult);

                result.ElapsedTime = stopWatch.Elapsed;
                if (powershell.InvocationStateInfo.State == PSInvocationState.Failed)
                {
                    result.Error = powershell.InvocationStateInfo.Reason.GetType().Name + ": " +
                        powershell.InvocationStateInfo.Reason.Message;
                    result.ExitStatus = -1;
                }

                if (!string.IsNullOrEmpty(result.Error))
                {
                    if (serverCommand.ThrowError)
                        throw new CommandResultException(
                           $"Command {serverCommand.Text} returned the following error(s): {result.Error}");
                    result.ExitStatus = -1;
                }
            }

            return result;
        }

        private string Verbose_DataAdded(object sender, DataAddedEventArgs e)
        {
            if (sender is PSDataCollection<VerboseRecord> records)
            {
                return string.Join(Environment.NewLine + Environment.NewLine,
                        records.Select(r => r.Message));
            }

            return string.Empty;
        }
        private string Progress_DataAdded(object sender, DataAddedEventArgs e)
        {
            if (sender is PSDataCollection<ProgressRecord> records)
            {
                return string.Join(Environment.NewLine + Environment.NewLine,
                        records.Select(r => r.CurrentOperation + ": " + r.StatusDescription));
            }

            return string.Empty;
        }
        private string Debug_DataAdded(object sender, DataAddedEventArgs e)
        {
            if (sender is PSDataCollection<DebugRecord> records)
            {
                return string.Join(Environment.NewLine + Environment.NewLine,
                        records.Select(r => r.Message));
            }

            return string.Empty;
        }
        private string Warning_DataAdded(object sender, DataAddedEventArgs e)
        {
            if (sender is PSDataCollection<WarningRecord> records)
            {
                return string.Join(Environment.NewLine + Environment.NewLine,
                        records.Select(r => "WARN: " + r.Message));
            }

            return string.Empty;
        }
        private string Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            if (sender is PSDataCollection<ErrorRecord> records)
            {
                return string.Join(Environment.NewLine + Environment.NewLine,
                        records.Select(r => r.Exception.Message + ": " + Environment.NewLine +
                            r.ScriptStackTrace)
                        );
            }

            return string.Empty;
        }
    }
}
