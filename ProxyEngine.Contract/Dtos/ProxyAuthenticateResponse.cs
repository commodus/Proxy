using ProxyEngine.Contract.Dtos.Base;

namespace ProxyEngine.Contract.Dtos
{
    public class ProxyAuthenticateResponse : ProxyBaseResponse
    {
        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Mail { get; set; }

        public string Mobile { get; set; }

        public bool IsAdmin { get; set; }
    }
}
