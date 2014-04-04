using System.Collections.Generic;
using RepositoryExample.Entity;

namespace RepositoryExample.Service
{
    public interface ICharacterService
    {
        #region Methods

        /// <summary>
        /// validate updates to a Character
        /// </summary>
        void Update(Character character);

        /// <summary>
        /// Retrieve a Character with the specified Name
        /// </summary>
        Character GetByName(string name);

        /// <summary>
        /// insert or update a Character
        /// </summary>
        Character Insert(Character character);

        /// <summary>
        /// Retrieve all Characters
        /// </summary>
        ICollection<Character> GetAll();

        /// <summary>
        /// Retrieve all Characters for the given Account
        /// </summary>
        ICollection<Character> GetByAccount(Account account);

        #endregion
    }
}