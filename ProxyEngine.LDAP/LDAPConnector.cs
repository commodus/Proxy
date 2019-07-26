using Novell.Directory.Ldap;
using ProxyEngine.LDAP.Dtos;
using System;

namespace ProxyEngine.LDAP
{
    public class LDAPConnector : ILDAPConnector
    {
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string MailAttribute = "mail";
        private const string MobileAttribute = "mobile";
        private const string FirstNameAtribute = "givenName";
        private const string LastNameAttribute = "sn";

        public LDAPConnector() { }

        public LDAPResponse Connect(UserInfo userInfo, LDAPConfig config)
        {
            LdapEntry user;
            LDAPResponse response = new LDAPResponse();

            try
            {
                #region Check Parameters
                if (string.IsNullOrEmpty(config.Url))
                    throw new Exception("InvalidLDAPUrlConfigParameter");

                if (string.IsNullOrEmpty(config.BindDn))
                    throw new Exception("InvalidLDAPBindDnConfigParameter");

                if (string.IsNullOrEmpty(config.BindCredentials))
                    throw new Exception("InvalidLDAPBindCridentialsConfigParameter");

                if (string.IsNullOrEmpty(config.SearchBase))
                    throw new Exception("InvalidLDAPSearchFilterConfigParameter");

                if (string.IsNullOrEmpty(config.SearchFilter))
                    throw new Exception("InvalidLDAPSearchBaseConfigParameter");

                if (string.IsNullOrEmpty(config.AdminCn))
                    throw new Exception("InvalidLDAPAdminCNConfigParameter");

                if (string.IsNullOrEmpty(userInfo.UserName))
                    throw new Exception("UserNameMustBeDefined");

                if (string.IsNullOrEmpty(userInfo.Password))
                    throw new Exception("PasswordMustBeDefined");

                #endregion

                var connection = new LdapConnection
                {
                    SecureSocketLayer = false,
                    ConnectionTimeout = config.ConnectionTimeOut
                };

                connection.Connect(config.Url, LdapConnection.DEFAULT_PORT);
                connection.Bind(config.BindDn, config.BindCredentials);

                var searchFilter = string.Format(config.SearchFilter, userInfo.UserName);
                var result = connection.Search(
                    config.SearchBase,
                    LdapConnection.SCOPE_SUB,
                    searchFilter,
                    new[] { MemberOfAttribute, DisplayNameAttribute, SAMAccountNameAttribute, MailAttribute, MobileAttribute,
                    FirstNameAtribute,LastNameAttribute},
                    false
                );

                try
                {
                    user = result.Next();

                    if (user != null)
                    {
                        connection.Bind(user.DN, userInfo.Password);

                        if (connection.Bound)
                        {
                            response.IsSuccess = true;
                            response.DisplayName = user.getAttribute(DisplayNameAttribute)?.StringValue ?? "";
                            response.Username = user.getAttribute(SAMAccountNameAttribute)?.StringValue ?? "";
                            response.Mail = user.getAttribute(MailAttribute)?.StringValue ?? "";
                            response.Mobile = user.getAttribute(MobileAttribute)?.StringValue ?? "";
                            response.FirstName = user.getAttribute(FirstNameAtribute)?.StringValue ?? "";
                            response.LastName = user.getAttribute(LastNameAttribute)?.StringValue ?? "";
                            response.IsAdmin = false;
                        }
                        else
                        {
                            throw new Exception("LDAPPasswordInCorrect");
                        }
                    }
                    else
                    {
                        throw new Exception("LDAPUserNotFound");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
