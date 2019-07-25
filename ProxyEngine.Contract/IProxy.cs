using ProxyEngine.Contract.Dtos;
using ProxyEngine.Contract.Enums;

namespace ProxyEngine.Contract
{
    public interface IProxy
    {
        ConnectionType ConnectionType { get; }

        void Initialize(ProxySettings proxySettings);
        ProxyAuthenticateResponse Authenticate(ProxyAuthenticateRequest proxyAuthenticateRequest);
    }
}
