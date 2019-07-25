using Microsoft.Extensions.DependencyInjection;
using ProxyEngine.LDAP;

namespace ProxyTest
{
    public static class LDAPInstaller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void LDAPInstall(this IServiceCollection services)
        {
            services.AddScoped<ILDAPConnector, LDAPConnector>();
        }
    }
}
