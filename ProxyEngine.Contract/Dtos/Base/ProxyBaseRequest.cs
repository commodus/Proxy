using System;

namespace ProxyEngine.Contract.Dtos.Base
{
    [Serializable]
    public abstract class ProxyBaseRequest
    {
        public Guid? TrackId;
        public DateTime TransactionDateTime;
        public DateTime AcceptanceDateTime;
    }
}
