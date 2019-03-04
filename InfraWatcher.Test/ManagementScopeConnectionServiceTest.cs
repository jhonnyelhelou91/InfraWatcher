using InfraWatcher.Core.Exceptions;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Services;
using InfraWatcher.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;

namespace InfraWatcher.Test
{
    [TestClass]
    public class ManagementScopeConnectionServiceTest
    {
        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Connect_WhenCredentialsNull_ThrowsArgumentNullException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = null,
                Host = "tatata"
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Connect_WhenHostNull_ThrowsArgumentNullException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default"
                },
                Host = null
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Connect_WhenUsernameNull_ThrowsArgumentNullException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = null
                },
                Host = "10.1.10.1"
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenHostDoesNotExist_ThrowsServerConnectionException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default"
                },
                Host = "tatata"
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenConnectionTimeOut_ThrowsWindowsOperationTimeoutException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default",
                    Password = null
                },
                Host = "10.1.10.1",
                Timeout = TimeSpan.Zero
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenInvalidUsername_ThrowsServerConnectionException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "****"
                },
                Host = "10.1.10.1"
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenInvalidPassword_ThrowsServerConnectionException()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default",
                    Password = "blabla"
                },
                Host = "10.1.10.1"
            });
        }

        [TestMethod]
        [TestCategory("WindowsConnection")]
        public void Connect_WhenValidUserAndPassword_IsConnected()
        {
            IServerConnectionService connection = new ManagementScopeConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default",
                    Password = ""
                },
                Host = "10.1.10.1"
            });

            Assert.IsTrue(connection.IsConnected);
        }
    }
}
