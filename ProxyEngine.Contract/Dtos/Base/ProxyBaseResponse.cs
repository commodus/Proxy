using ProxyEngine.Contract.Enums;

namespace ProxyEngine.Contract.Dtos.Base
{
    public abstract class ProxyBaseResponse
    {
        public string ResponseCode;
        public string ReserveInfo;
        public ProxyStatus Status;
        public string Message;
    }
}
