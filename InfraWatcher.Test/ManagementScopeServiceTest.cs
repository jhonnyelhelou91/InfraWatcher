using InfraWatcher.Common.Providers;
using InfraWatcher.Common.Services;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Providers;
using InfraWatcher.Core.Services;
using InfraWatcher.Connections.WMI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InfraWatcher.Core.Models.Configuration;

namespace InfraWatcher.Test
{
    [TestClass]
    public class ManagementScopeServiceTest
    {
        private static readonly ServerConnection ServerConnection = new ServerConnection
        {
            Host = "10.1.10.1",
            Timeout = TimeSpan.FromSeconds(10),
            Credential = new ServerCredential
            {
                Username = "default",
                Password = null
            }
        };
        private static IServerConnectionService ServerConnectionService;
        private static IServerInfoService ServerInfoService;
        private static IServerMemoryService ServerMemoryService;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Environment.CurrentDirectory += "\\..\\..\\..\\";
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.management-scope.json", optional: true, reloadOnChange: true);

            var serviceCollection = new ServiceCollection()
                .Configure<InfraWatcherOption>(builder.Build())
                .AddScoped<ICommandProvider, DefaultCommandProvider>()
                .AddScoped<IServerInfoService, ServerInfoService>()
                .AddScoped<IServerMemoryService, ServerMemoryService>()
                .AddScoped<IServerOperatingSystemService, ServerOperatingSystemService>()
                .AddScoped<IServerProcessorService, ServerProcessorService>()
                .AddScoped<IServerConnectionService, ManagementScopeConnectionService>();

            var serviceProvider = serviceCollection
                .BuildServiceProvider();

            ServerConnectionService = serviceProvider.GetService<IServerConnectionService>();
            ServerConnectionService.Connect(ServerConnection);
            ServerInfoService = serviceProvider.GetService<IServerInfoService>();
            ServerMemoryService = serviceProvider.GetService<IServerMemoryService>();
        }

        [TestMethod]
        [TestCategory("Command")]
        [TestCategory("ManagementScope")]
        public void GetIPAddress_Windows_ReturnIP()
        {
            var ipAddress = ServerInfoService.GetIPAddress(ServerConnectionService);

            Assert.AreEqual(ServerConnectionService.ServerConnection.Host, ipAddress.ToString());
        }

        [TestMethod]
        [TestCategory("Command")]
        [TestCategory("ManagementScope")]
        public void GetTotalMemory_Windows_ReturnTotalMemory()
        {
            var totalMemory = ServerMemoryService.GetTotalMemory(ServerConnectionService);

            Assert.IsTrue(totalMemory > 0);
        }
    }
}
