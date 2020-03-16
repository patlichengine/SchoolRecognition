using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.Models
{
    public class ViewModels
    {
    }

    public class SchoolPaymentViewModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Pin(pinid to be hidden)")]
        public Guid PinID { get; set; }
        [Display(Name = "School(schoolid to be hidden)")]
        public Guid SchoolID { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Receipt Number")]
        public string ReceiptNo { get; set; }
        [Display(Name = "Scanned Receipt")]
        public IFormFile ReceiptImage { get; set; }
        public string DateCreated { get; set; }
        [Display(Name = "Created by(createdbyid to be hidden)")]
        public Guid CreatedBy { get; set; }
       
    }

    public class RecognitionTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ForgotViewModel
    {
        [Required] [Display(Name = "Email")] public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class ChangeUserPasswordViewModel
    {
        [Key] public Guid UserID { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The confirm password does not match.")]
        public string ConfirmNewPassword { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
