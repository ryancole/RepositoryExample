using System.Collections.Generic;
using RepositoryExample.Entity;

namespace RepositoryExample.Service
{
    public interface ICharacterService
    {
        #region Methods

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
        IReadOnlyCollection<Character> GetAll();

        #endregion
    }
}