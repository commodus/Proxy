using ProxyEngine.Contract.Dtos.Base;
using System;

namespace ProxyEngine.Contract.Dtos
{
    [Serializable]
    public class ProxyAuthenticateRequest : ProxyBaseRequest
    {
        public ProxyUser User;
    }
}
