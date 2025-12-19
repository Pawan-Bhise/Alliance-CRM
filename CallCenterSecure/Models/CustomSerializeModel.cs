using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] RoleName { get; set; }
        public string UserRoleName { get; set; }
        public int UserRoleId { get; set; }
    }
}