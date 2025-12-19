using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter user id")]
        public string UserName { get; set; }
        [Required (ErrorMessage ="Please enter password")]
        public string Password { get; set; }
    }
}