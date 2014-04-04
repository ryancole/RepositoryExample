using System.ComponentModel.DataAnnotations;

namespace RepositoryExample.Web.Models
{
    public class AccountLoginViewModel
    {
        #region Properties

        [Required, EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public string Password { get; set; }

        #endregion
    }
}