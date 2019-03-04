using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace InfraWatcher.Core.Extensions
{
    public static class StringExtensions
    {
        public static SecureString ToSecureString(this string str)
        {
            SecureString secureString = new SecureString();
            if (string.IsNullOrEmpty(str))
                return secureString;

            foreach(var character in str)
                secureString.AppendChar(character);

            return secureString;
        }
    }
}
