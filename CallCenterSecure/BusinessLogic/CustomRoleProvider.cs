using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.BusinessLogic
{
    public class CustomRoleProvider
    {
        public string GetRoleNameById(int roleId)
        {
            string roleName = string.Empty;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                roleName = (from role in dbContext.Roles where role.RoleId == roleId
                            select role.RoleName).SingleOrDefault();
                
            }
            return roleName;
        }

        public List<Role> GetAllRoles()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<Role> roleList = new List<Role>();
                roleList = (from role in dbContext.Roles select role).ToList();
                return roleList;
            }
        }
    }
}