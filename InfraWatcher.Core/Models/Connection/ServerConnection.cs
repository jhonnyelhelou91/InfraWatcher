using System;

namespace InfraWatcher.Core.Models.Connection
{
    public class ServerConnection
    {
        public string Host { get; set; }
        public int? Port { get; set; }
        public ServerCredential Credential { get; set; }
        public TimeSpan? Timeout { get; set; }
    }
}
