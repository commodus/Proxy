using ProxyBase;
using ProxyEngine.Contract;
using ProxyEngine.Contract.Dtos;
using ProxyEngine.Contract.Enums;
using ProxyEngine.LDAP.Dtos;
using System;

namespace ProxyEngine.LDAP
{
    public class LDAPProxy : Proxy
    {
        private readonly ILDAPConnector _connector;

        public LDAPProxy(ILDAPConnector connector)
        {
            this._connector = connector;
        }

        public override ConnectionType ConnectionType
        {
            get
            {
                return ConnectionType.LDAP;
            }
        }

        public override void Initialize(ProxySettings proxySettings)
        {
            base.Initialize(proxySettings);
        }

        public override ProxyAuthenticateResponse Authenticate(ProxyAuthenticateRequest request)
        {
            ProxyAuthenticateResponse response = new ProxyAuthenticateResponse();
            try
            {
                LDAPConfig config = new LDAPConfig
                {
                    AdminCn = _proxySettings.Parameters["AdminCn"],
                    BindDn = _proxySettings.Parameters["BindDn"],
                    BindCredentials = _proxySettings.Parameters["BindCredentials"],
                    SearchBase = _proxySettings.Parameters["SearchBase"],
                    SearchFilter = _proxySettings.Parameters["SearchFilter"],
                    ConnectionTimeOut = _proxySettings.Timeout.GetValueOrDefault(),
                    Url = _proxySettings.Url
                };

                UserInfo user = new UserInfo
                {
                    UserName = request.User.Username,
                    Password = request.User.Password
                };

                var result = SendRequest(user, o => _connector.Connect(user, config));

                if (!result.IsSuccess)
                {
                    return new ProxyAuthenticateResponse
                    {
                        Status = ProxyStatus.Failure,
                        ResponseCode = "LDAPLoginFailed",
                        Message = result.ErrorMessage
                    };
                }

                response = new ProxyAuthenticateResponse
                {
                    IsAdmin = result.IsAdmin,
                    Username = result.Username,
                    DisplayName = result.DisplayName,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Mail = result.Mail,
                    Mobile = result.Mobile,
                    Status = ProxyStatus.Success,
                    ResponseCode = "Success"
                };

                return response;
            }
            catch (Exception ex)
            {
                response.Status = ProxyStatus.Failure;
                response.ResponseCode = "LDAPLoginFailed";
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
