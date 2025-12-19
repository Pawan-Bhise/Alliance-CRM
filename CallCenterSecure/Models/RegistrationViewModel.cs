using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class RegistrationViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter User ID")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [MinLength(8, ErrorMessage = "Minimun password length should be 8 character")]
        [MaxLength(15, ErrorMessage = "Maximun password length should be 15 character")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string ActivationCode { get; set; }
        [Required]
        public int RoleId { get; set; }

        public List<DataAccess.Role> RoleList { get; set; }

        public List<User> UsersList { get; set; }
        public bool IsActive { get; set; }
    }
}