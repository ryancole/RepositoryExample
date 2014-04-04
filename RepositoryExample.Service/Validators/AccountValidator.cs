using FluentValidation;
using RepositoryExample.Entity;
using RepositoryExample.Repository;

namespace RepositoryExample.Service
{
    internal class AccountValidator : AbstractValidator<Account>
    {
        private readonly IRepository<Account> m_accounts;

        public AccountValidator(IRepository<Account> accounts)
        {
            m_accounts = accounts;

            RuleSet("Insert", () =>
            {

                // email validation rules
                RuleFor(m => m.Email).NotEmpty()
                                     .Must(EmailIsAvailable).WithMessage("{0} is not available", m => m.Email);

            });

            // password validation rules
            RuleFor(m => m.Password).NotEmpty();
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
