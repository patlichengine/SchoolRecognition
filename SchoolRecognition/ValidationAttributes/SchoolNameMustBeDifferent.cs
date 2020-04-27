using SchoolRecognition.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRecognition.ValidationAttributes
{
    public class SchoolNameMustBeDifferent : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var user = (SchoolsDto)validationContext.ObjectInstance;
            if (user.PhoneNo == user.PhoneNo)
            {
                return new ValidationResult(
                    ErrorMessage,
                    new[] { nameof(SchoolsDto) }
                    );
            }

            return ValidationResult.Success;
        }
    }
}
