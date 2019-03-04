using InfraWatcher.Common.Providers;
using InfraWatcher.Common.Services;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Providers;
using InfraWatcher.Core.Services;
using InfraWatcher.Ssh;
using InfraWatcher.Windows;
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
    public class LinuxServiceTest
    {
        private static readonly ServerConnection ServerConnection = new ServerConnection
        {
            Host = "10.10.10.10",
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
        private static ICommandProvider CommandProvider;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            Environment.CurrentDirectory += "\\..\\..\\..\\";
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var serviceCollection = new ServiceCollection()
                .Configure<InfraWatcherOption>(builder.Build())
                .AddScoped<ICommandProvider, DefaultCommandProvider>()
                .AddScoped<IServerInfoService, ServerInfoService>()
                .AddScoped<IServerMemoryService, ServerMemoryService>()
                .AddScoped<IServerOperatingSystemService, ServerOperatingSystemService>()
                .AddScoped<IServerProcessorService, ServerProcessorService>()
                .AddSingleton<IOperatingSystemProvider, TTLOperatingSystemProvider>()
                .AddScoped<IServerConnectionService>((provider) =>
                {
                    var osProvider = provider.GetService<IOperatingSystemProvider>();
                    var operatingSystem = osProvider.Detect(ServerConnection);

                    if (operatingSystem.OperationSystem == "Linux")
                        return new SshConnectionService();
                    else if (operatingSystem.OperationSystem == "Windows")
                        return new ManagementScopeConnectionService();
                    else
                        throw new ArgumentException("Unsupported operating system");
                });

            var serviceProvider = serviceCollection
                .BuildServiceProvider();

            ServerConnectionService = serviceProvider.GetService<IServerConnectionService>();
            ServerConnectionService.Connect(ServerConnection);
            ServerInfoService = serviceProvider.GetService<IServerInfoService>();
            ServerMemoryService = serviceProvider.GetService<IServerMemoryService>();
            CommandProvider = serviceProvider.GetService<ICommandProvider>();
        }

        [TestMethod]
        public void GetIPAddress_Linux_ReturnIP()
        {
            var ipAddress = ServerInfoService.GetIPAddress(ServerConnectionService);

            Assert.AreEqual(ServerConnectionService.ServerConnection.Host, ipAddress.ToString());
        }

        [TestMethod]
        public void GetTotalMemory_LinuxOperatingSystem_UseLinuxCommand()
        {
            var totalMemory = ServerMemoryService.GetTotalMemory(ServerConnectionService);

            Assert.IsTrue(totalMemory > 0);
        }
    }
}
