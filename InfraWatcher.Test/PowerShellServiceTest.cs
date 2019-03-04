using InfraWatcher.Common.Providers;
using InfraWatcher.Common.Services;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Providers;
using InfraWatcher.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InfraWatcher.Core.Models.Configuration;
using InfraWatcher.Connections.RemotePowerShell;

namespace InfraWatcher.Test
{
    [TestClass]
    public class PowerShellServiceTest
    {
        private static readonly ServerConnection ServerConnection = new ServerConnection
        {
            Port = 443,
            Host = "10.1.10.1",
            Timeout = TimeSpan.FromSeconds(10),
            Credential = new ServerCredential
            {
                Username = "default",
                Password = ""
            }
        };
        private static IServerConnectionService ServerConnectionService;
        private static IServerInfoService ServerInfoService;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Environment.CurrentDirectory += "\\..\\..\\..\\";
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.powershell.json", optional: true, reloadOnChange: true);

            var serviceCollection = new ServiceCollection()
                .Configure<InfraWatcherOption>(builder.Build())
                .AddScoped<ICommandProvider, DefaultCommandProvider>()
                .AddScoped<IServerInfoService, ServerInfoService>()
                .AddScoped<IServerMemoryService, ServerMemoryService>()
                .AddScoped<IServerOperatingSystemService, ServerOperatingSystemService>()
                .AddScoped<IServerProcessorService, ServerProcessorService>()
                .AddScoped<IServerConnectionService, PowerShellConnectionService>();

            var serviceProvider = serviceCollection
                .BuildServiceProvider();

            ServerConnectionService = serviceProvider.GetService<IServerConnectionService>();
            ServerConnectionService.Connect(ServerConnection);
            ServerInfoService = serviceProvider.GetService<IServerInfoService>();
        }
        
        [TestMethod]
        public void GetSerialNumber_PowerShell_ReturnSerialNumber()
        {
            var serialNumber = ServerInfoService.GetSerialNumber(ServerConnectionService);

            Assert.IsNotNull(serialNumber);
        }
    }
}
