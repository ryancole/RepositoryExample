using System.Linq;
using System.Data.Entity;
using FluentValidation;
using RepositoryExample.Entity;

namespace RepositoryExample.Data
{
    internal class AccountValidator : AbstractValidator<Account>
    {
        private readonly IDbSet<Account> m_accounts;

        public AccountValidator(IDbSet<Account> accounts)
        {
            m_accounts = accounts;

            // email validation rules
            RuleFor(m => m.Email).NotEmpty();

            // password validation rules
            RuleFor(m => m.Password).NotEmpty();

            RuleSet("insert", () =>
            {

                // email validation rules
                RuleFor(m => m.Email).NotEmpty()
                                     .Must(EmailIsAvailable).WithMessage("{0} is not available", m => m.Email);

            });
        }

        #region Methods

        private bool EmailIsAvailable(string email)
        {
            if (m_accounts.Count(m => m.Email.ToLower() == email.ToLower()) <= 0)
                return true;

            return false;
        }

        #endregion
    }
}
