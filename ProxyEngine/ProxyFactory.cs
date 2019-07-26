using ProxyEngine.Contract;
using ProxyEngine.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProxyEngine
{
    public class ProxyFactory : IProxyFactory
    {
        private IEnumerable<IProxy> _proxy;
    
        public ProxyFactory(IEnumerable<IProxy> proxy)
        {
            this._proxy = proxy;
        }

        public virtual IProxy GetProxy(int moduleId, ProxyRequest proxyRequest)
        {
            var proxySettings = GetProxySettings(moduleId, proxyRequest);
            var proxy = this._proxy.ToList().FirstOrDefault(p => p.ConnectionType == proxySettings.ConnectionType);

            if (proxy != null)
            {
                proxy.Initialize(proxySettings);
                return proxy;
            }

            throw new ApplicationException("Proxy not found for :" + proxySettings.ConnectionType);
        }

        public IProxy GetProxy(ConnectionType connectionType, ProxyRequest proxyRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kullanılabilir proxy type'larının listesini verir
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IProxy> GetAllProxyProxies()
        {
            return _proxy;
        }

        private ProxySettings GetProxySettings(int moduleId, ProxyRequest proxyRequest)
        {
            ProxySettings proxySettings;
            try
            {
                // TODO : Get proxy settings from the database or other file with moduleId
                proxySettings = new ProxySettings();
                proxySettings.ConnectionType = ConnectionType.LDAP;
                proxySettings.Url = "";
                proxySettings.Timeout = 60000;

                var parameters = new Dictionary<string, string>();
                parameters.Add("BindDn", @"");
                parameters.Add("BindCredentials", @"");
                parameters.Add("SearchBase", @"");
                parameters.Add("SearchFilter", @"");
                parameters.Add("AdminCn", @"");

                proxySettings.Parameters = parameters;
                return proxySettings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
