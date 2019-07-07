namespace BLL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Persistence.Models.Identity;

    public class AppUserViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Rank { get; set; }

        public bool Banned { get; set; } // banned users cannot create Questionnaires

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var user = validationContext.GetService<UserManager<AppUser>>()
                .FindByNameAsync(((AppUserViewModel)validationContext.ObjectInstance).FirstName);
            if (user.Result != null)
            {
                yield return new ValidationResult("You suck", new string[] { "FirstName" });
            }
        }
    }
}
