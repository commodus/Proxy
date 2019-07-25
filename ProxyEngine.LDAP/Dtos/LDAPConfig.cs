namespace ProxyEngine.LDAP.Dtos
{
    public class LDAPConfig
    {
        public string Url { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string AdminCn { get; set; }
        public int ConnectionTimeOut { get; set; }
    }
}
