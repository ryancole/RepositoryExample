using RepositoryExample.Entity;

namespace RepositoryExample.Service
{
    public interface IAccountService
    {
        #region Methods

        /// <summary>
        /// Insert a new Account
        /// </summary>
        Account Insert(Account account);

        /// <summary>
        /// Retrieve a single Account by Email
        /// </summary>
        Account GetByEmail(string email);

        /// <summary>
        /// Retrieve a single Account by Email and Password
        /// </summary>
        Account GetByEmailAndPassword(string email, string password);

        #endregion
    }
}