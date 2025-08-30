using PMApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Models.DTO.Requests
{
    public class UserRequest : IValidatableObject
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required2]
        [MinLength(8)]
        public string Password { get; set; }

        [Required2]
        [MinLength(8)]
        public string RepeatPassword { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public string RoleName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            if (Password != RepeatPassword)
            {
                result.Add(new ValidationResult("Passwords do not match", new string[] { "Password" }));
            }
            return result;
        }

        public class Required2Attribute : RequiredAttribute
        {
            public override bool IsValid(object value)
            {
                return base.IsValid(value);
            }
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                return base.IsValid(value, validationContext);
            }
        }

    }
}
