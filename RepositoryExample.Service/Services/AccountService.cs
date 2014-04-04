using System.Linq;
using System.Text;
using System.Security.Cryptography;
using FluentValidation;
using RepositoryExample.Entity;
using RepositoryExample.Repository;

namespace RepositoryExample.Service
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> m_accounts;

        public AccountService(IRepository<Account> accounts)
        {
            m_accounts = accounts;
        }

        #region Methods

        /// <summary>
        /// Insert a new Account
        /// </summary>
        public Account Insert(Account account)
        {
            // hash the given password
            account.Password = GetPasswordHash(account.Password);

            // validate the new account
            var validator = new AccountValidator(m_accounts);
            var validationresults = validator.Validate(account, ruleSet: "default,Insert");

            if (!validationresults.IsValid)
                throw new ValidationException(validationresults.Errors);

            return m_accounts.Insert(account);
        }

        /// <summary>
        /// Retrieve a single Account by Email
        /// </summary>
        public Account GetByEmail(string email)
        {
            return m_accounts.Where(m => m.Email.ToLower() == email.ToLower())
                             .SingleOrDefault();
        }

        /// <summary>
        /// Retrieve a single Account by Email and Password
        /// </summary>
        public Account GetByEmailAndPassword(string email, string password)
        {
            var hashedPassword = GetPasswordHash(password);

            return m_accounts.Where(m => m.Email.ToLower() == email.ToLower() && m.Password == hashedPassword)
                             .SingleOrDefault();
        }

        #endregion

        #region Utility Methods

        private string GetPasswordHash(string password)
        {
            using (var hasher = MD5.Create())
            {
                var builder = new StringBuilder();
                var bytes = hasher.ComputeHash(Encoding.Default.GetBytes(password));

                for (int x = 0; x < bytes.Length; x++)
                {
                    builder.Append(bytes[x].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        #endregion
    }
}
