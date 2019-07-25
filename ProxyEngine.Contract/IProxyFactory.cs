using ProxyEngine.Contract.Enums;
using System.Collections.Generic;

namespace ProxyEngine.Contract
{
    public interface IProxyFactory
    {
        IProxy GetProxy(int moduleId, ProxyRequest proxyRequest);
        IProxy GetProxy(ConnectionType connectionType, ProxyRequest proxyRequest);

        /// <summary>
        /// Kullanılabilir proxy type'larının listesini verir
        /// </summary>
        /// <returns></returns>
        IEnumerable<IProxy> GetAllProxyProxies();
    }
}
