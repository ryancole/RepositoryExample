using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepositoryExample.Web.Models
{
    public class AccountRegisterViewModel : IValidatableObject
    {
        #region Properties

        [Required, EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public string PasswordRepeat { get; set; }

        #endregion

        #region Methods

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // enforce password matching
            if (Password != PasswordRepeat)
            {
                yield return new ValidationResult(string.Format("Password and Password Repeat must match"), new[]
                {
                    "Password"
                });
            }
        }

        #endregion
    }
}