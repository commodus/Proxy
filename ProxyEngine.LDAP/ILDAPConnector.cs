using ProxyEngine.LDAP.Dtos;

namespace ProxyEngine.LDAP
{
    public interface ILDAPConnector
    {
        LDAPResponse Connect(UserInfo userInfo, LDAPConfig config);
    }
}
