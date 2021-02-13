using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class OwnerRegisterModel
    {
        [Required(ErrorMessage ="User name is required")]
        [Display(Name ="User name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email name is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100,ErrorMessage ="The {0} must be at least {2} charactes long.",MinimumLength=8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage = "The password and confirmation password do not match!")]
        public string ConfirmPassword { get; set; }

    }

    public class RenterRegisterModel
    {
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email name is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Start date is required")]
        //[DataType(DataType.DateTime)]
        //[Display(Name = "Start date")]
        //public DateTime start_date { get; set; }
        //[Required(ErrorMessage = "End date is required")]
        //[Range(typeof(DateTime), "31/01/2021", "31/12/2025",
        //ErrorMessage = "Value for {0} must be between {1} and {2}")]
        //[DataType(DataType.DateTime)]
        //[Display(Name = "End date")]
        //public DateTime end_date { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} charactes long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match!")]
        public string ConfirmPassword { get; set; }

    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} charactes long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} charactes long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match!")]
        public string ConfirmPassword { get; set; }
    }
}