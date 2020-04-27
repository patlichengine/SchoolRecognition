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

        public Guid CenterNoID { get; set; }
        [Display(Name = "Center Number")]
        public string CenterNo { get; set; }
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Display(Name = "School Category")]
        public string SchoolCategory { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }

        [Display(Name = "Year of Establishment")]
        public string YearEstablished { get; set; }

        public Guid LgID { get; set; }

        public Guid SchoolCategoryID { get; set; }
        public Guid RecognitionTypeID { get; set; }

        public Guid OfficeID { get; set; }

      
       
        public Guid PinID { get; set; }
       
        public Guid SchoolID { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Receipt Number")]
        public string ReceiptNo { get; set; }

      

        [Display(Name = "Scanned Receipt")]
        public IFormFile ReceiptImage { get; set; }
        public string DateCreated { get; set; }
        
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
