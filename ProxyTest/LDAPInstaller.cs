using Microsoft.Extensions.DependencyInjection;
using ProxyEngine.LDAP;

namespace ProxyTest
{
    public static class LDAPInstaller
    {
        public static void LDAPInstall(this IServiceCollection services)
        {
            services.AddScoped<ILDAPConnector, LDAPConnector>();
        }
    }
}
