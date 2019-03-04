using InfraWatcher.Core.Exceptions;
using InfraWatcher.Core.Models.Connection;
using InfraWatcher.Core.Services;
using InfraWatcher.Connections.SSH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;

namespace InfraWatcher.Test
{
    [TestClass]
    public class SSHConnectionServiceTest
    {
        [TestMethod]
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Connect_WhenCredentialsNull_ThrowsArgumentNullException()
        {
            IServerConnectionService connection = new SSHConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = null,
                Host = "tatata"
            });
        }

        [TestMethod]
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Connect_WhenHostNull_ThrowsArgumentNullException()
        {
            IServerConnectionService connection = new SSHConnectionService();
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
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Connect_WhenUsernameNull_ThrowsArgumentNullException()
        {
            IServerConnectionService connection = new SSHConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = null
                },
                Host = "10.10.10.10"
            });
        }

        [TestMethod]
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenHostDoesNotExist_ThrowsServerConnectionException()
        {
            IServerConnectionService connection = new SSHConnectionService();
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
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenConnectionTimeOut_ThrowsSshOperationTimeoutException()
        {
            IServerConnectionService connection = new SSHConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default",
                    Password = null
                },
                Host = "10.10.10.10",
                Timeout = TimeSpan.Zero
            });
        }

        [TestMethod]
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenInvalidUsername_ThrowsServerConnectionException()
        {
            IServerConnectionService connection = new SSHConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "****"
                },
                Host = "10.10.10.10"
            });
        }

        [TestMethod]
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        [ExpectedException(typeof(ServerConnectionException))]
        public void Connect_WhenInvalidPassword_ThrowsServerConnectionException()
        {
            IServerConnectionService connection = new SSHConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default",
                    Password = "blabla"
                },
                Host = "10.10.10.10"
            });
        }

        [TestMethod]
        [TestCategory("Connection")]
        [TestCategory("SSH")]
        public void Connect_WhenValidUserAndPassword_IsConnected()
        {
            IServerConnectionService connection = new SSHConnectionService();
            connection.Connect(new ServerConnection
            {
                Credential = new ServerCredential
                {
                    Username = "default",
                    Password = ""
                },
                Host = "10.10.10.10"
            });

            Assert.IsTrue(connection.IsConnected);
        }
    }
}
