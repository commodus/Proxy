using ProxyEngine.Contract.Enums;
using System;
using System.Collections.Generic;

namespace ProxyEngine.Contract
{
    public class ProxySettings
    {
        public ConnectionType ConnectionType;
        public string Url;
        public int? Timeout;
        public IDictionary<string, string> Parameters;

        public ProxySettings()
        {
            this.Parameters = new Dictionary<string, string>();
        }

        public string GetValue(string key)
        {
            return this.Parameters.ContainsKey(key) ? this.Parameters[key] : string.Empty;
        }

        public string Username
        {
            get
            {
                if (!this.Parameters.ContainsKey(ConnectionParameters.UsernameKey))
                {
                    throw new ApplicationException("Username key not found in proxy parameter collection.");
                }

                return this.Parameters[ConnectionParameters.UsernameKey];
            }
        }

        public string Password
        {
            get
            {
                if (!this.Parameters.ContainsKey(ConnectionParameters.PasswordKey))
                {
                    throw new ApplicationException("Password key not found in proxy parameter collection.");
                }

                return this.Parameters[ConnectionParameters.PasswordKey];
            }
        }
    }
}
