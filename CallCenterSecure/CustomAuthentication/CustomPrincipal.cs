using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CallCenter.CustomAuthentication
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }

        public string UserRoleName { get; set; }
        public int UserRoleId { get; set; }
        #endregion
        public IIdentity Identity
        {
            get; private set;
        }
        public bool IsInRole(string role)
        {
            Roles = role.Split('|');
            if (Roles.Any(r => UserRoleName.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsInRoleId(int roleId)
        {
            if (Roles.Any(r => UserRoleId.Equals(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}