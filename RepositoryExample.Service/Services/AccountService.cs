using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Security.Cryptography;
using FluentValidation;
using RepositoryExample.Entity;

namespace RepositoryExample.Service
{
    public class AccountService : IAccountService
    {
        private readonly IDbSet<Account> m_accounts;

        public AccountService(IDbSet<Account> accounts)
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

            return m_accounts.Add(account);
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

            return m_accounts.Where(m => m.Email.ToLower() == email.ToLower())
                             .Where(m => m.Password == hashedPassword)
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
