using ProxyEngine.Contract;
using ProxyEngine.Contract.Dtos;
using ProxyEngine.Contract.Enums;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProxyBase
{
    public abstract class Proxy : IProxy
    {
        protected ProxySettings _proxySettings { get; private set; }
        public abstract ConnectionType ConnectionType { get; }

        public virtual void Initialize(ProxySettings proxySettings)
        {
            _proxySettings = proxySettings;
        }

        protected static bool TrustAllCertificatePolicy(object sender,
                                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #region IProxy

        public virtual ProxyAuthenticateResponse Authenticate(ProxyAuthenticateRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected T SendRequestAsync<T>(object request, Func<object, Task<T>> function)
        {
            // TODO : Log
            Stopwatch stopWatch = new Stopwatch();
            T response = default(T);
            Exception exception = null;
            stopWatch.Start();
            try
            {
                response = function(request).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                exception = e;
            }

            stopWatch.Stop();

            // TODO : Log
            if (exception != null)
                throw new Exception("Proxy error", exception);

            return response;
        }

        protected T SendRequest<T>(object request, Func<object, T> function)
        {
            // TODO : Log
            Stopwatch stopWatch = new Stopwatch();
            T response = default(T);
            Exception exception = null;
            stopWatch.Start();
            try
            {
                response = function(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            stopWatch.Stop();

            // TODO : Log
            if (exception != null)
                throw new Exception("Proxy error", exception);

            return response;
        }

        protected void SendRequest(object request, Action<object> function)
        {
            // TODO : Log
            Stopwatch stopWatch = new Stopwatch();
            Exception exception = null;
            stopWatch.Start();
            try
            {
                function(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            stopWatch.Stop();

            // TODO : Log
            if (exception != null)
                throw new Exception("Proxy error", exception);

        }
    }
}
