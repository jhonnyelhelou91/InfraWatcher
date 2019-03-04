using System.Collections.Generic;

namespace InfraWatcher.Core.Models.Configuration
{
    public class ServerTTLOption
    {
        public sbyte MaxRetry { get; set; }
        public IDictionary<string, long> TTL { get; set; }
    }
}