using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using FluentValidation;
using RepositoryExample.Entity;
using RepositoryExample.Repository;

namespace RepositoryExample.Service
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> m_characters;

        public CharacterService(IRepository<Character> characters)
        {
            m_characters = characters;
        }

        #region Methods

        /// <summary>
        /// Insert a new Character
        /// </summary>
        public Character Insert(Character character)
        {
            // character defaults
            character.Level = 1;
            character.Active = true;

            // death knights start at 55, though
            if (character.Class == CharacterClass.DeathKnight)
            {
                character.Level = 55;
            }

            return m_characters.Insert(character);
        }

        /// <summary>
        /// Retrieve a Character with the specified Name
        /// </summary>
        public Character GetByName(string name)
        {
            return m_characters.Where(m => m.Name.ToLower() == name.ToLower())
                               .SingleOrDefault();
        }

        /// <summary>
        /// Retrieve all Characters
        /// </summary>
        public ICollection<Character> GetAll()
        {
            return m_characters.All
                               .OrderByDescending(m => m.Active)
                               .ThenBy(m => m.Name)
                               .ToList();
        }

        /// <summary>
        /// Retireve all Characters for the given Account
        /// </summary>
        public ICollection<Character> GetByAccount(Account account)
        {
            return m_characters.Where(m => m.Account.AccountId == account.AccountId)
                               .OrderByDescending(m => m.Active)
                               .ThenBy(m => m.Name)
                               .ToList();
        }

        #endregion
    }
}
