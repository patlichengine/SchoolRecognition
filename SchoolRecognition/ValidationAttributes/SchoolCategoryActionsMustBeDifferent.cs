using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.ValidationAttributes
{
    public class SchoolCategoryActionsMustBeDifferent : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var user = (SchoolCategoryDto)validationContext.ObjectInstance;
            if (user.Name == user.Name)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(SchoolCategoryDto) }
                    );
            }

            return ValidationResult.Success;
        }
    }
}
