using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public int RoleId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is Required.")]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "last Name is Required.")]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required.")]
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is Required.")]
        [Display(Name = "Confirm Password: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should be the same.")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SuccessMessage { get; set; }

        //public string Gender { get; set; }
        public string UserImage { get; set; }
    }
}