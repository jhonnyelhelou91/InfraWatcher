using Microsoft.Extensions.Options;
using InfraWatcher.Core.Models.Configuration;
using InfraWatcher.Core.Providers;
using InfraWatcher.Core.Models.Connection;
using System;
using InfraWatcher.Core.Exceptions;
using System.Net.NetworkInformation;
using System.Linq;

namespace InfraWatcher.Common.Providers
{
    public class TTLOperatingSystemProvider : IOperatingSystemProvider
    {
        private readonly ServerTTLOption TTLOption;
        
        public TTLOperatingSystemProvider(IOptions<InfraWatcherOption> options)
        {
            this.TTLOption = options.Value.TTLOption;
        }

        public ServerOperatingSystem Detect(ServerConnection serverConnection)
        {
            if (serverConnection == null)
                throw new ArgumentNullException(nameof(serverConnection));
            if (string.IsNullOrEmpty(serverConnection.Host))
                throw new ArgumentNullException(nameof(serverConnection.Host));
            if (serverConnection.Credential == null)
                throw new ArgumentNullException(nameof(serverConnection.Credential));
            if (string.IsNullOrEmpty(serverConnection.Credential.Username))
                throw new ArgumentNullException(nameof(serverConnection.Credential.Username));

            try
            {
                var pingReply = RetryPing(serverConnection.Host, serverConnection.Timeout, TTLOption.MaxRetry);
                var matchedTTL = TTLOption.TTL.FirstOrDefault(kvp => pingReply.RoundtripTime == kvp.Value);
                if (matchedTTL.Value != pingReply.RoundtripTime)
                    throw new OperatingSystemProviderException($"TTL {pingReply.RoundtripTime} was not matched");
                else
                {
                    return new ServerOperatingSystem
                    {
                        OperationSystem = matchedTTL.Key
                    };
                }
            }
            catch (OperatingSystemProviderException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new OperatingSystemProviderException(exception.Message, exception);
            }
        }

        private PingReply RetryPing(string host, TimeSpan? timeout, int retryMax)
        {
            int retryCount = 1;
            do
            {
                var pinger = new Ping();
                try
                {
                    return GetPingReply(pinger, host, timeout);
                }
                catch (PingException)
                {
                    if (retryCount >= retryMax)
                    {
                        if (pinger != null)
                            pinger.Dispose();

                        throw;
                    }
                }
                catch (OperatingSystemProviderTimeoutException)
                {
                    if (retryCount >= retryMax)
                    {
                        if (pinger != null)
                            pinger.Dispose();

                        throw;
                    }
                }
                catch (OperatingSystemProviderException)
                {
                    if (retryCount >= retryMax)
                    {
                        if (pinger != null)
                            pinger.Dispose();

                        throw;
                    }
                }
                catch (Exception)
                {
                    if (pinger != null)
                        pinger.Dispose();
                    throw;
                }
                finally
                {
                    retryCount++;
                }
            } while (true);
        }
        private PingReply GetPingReply(Ping ping, string host, TimeSpan? timeout)
        {
            try
            {
                PingReply reply = timeout.HasValue ?
                    ping.Send(host, (int)timeout.Value.TotalMilliseconds) :
                    ping.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    ping.Dispose();
                    return reply;
                }
                else if (reply.Status == IPStatus.TimedOut)
                    throw new OperatingSystemProviderTimeoutException($"Ping timeout for host {host} and timeout {timeout}");
                else
                    throw new OperatingSystemProviderException($"Ping {reply.Status} for host {host} and timeout {timeout}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
