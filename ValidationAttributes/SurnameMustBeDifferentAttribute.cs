using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.ValidationAttributes
{
    public class SurnameMustBeDifferentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var user = (AccountsManipulationDto)validationContext.ObjectInstance;
            if (user.Surname == user.Othernames)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(AccountsManipulationDto) }
                    );
            }

            return ValidationResult.Success;
        }
    }
}
