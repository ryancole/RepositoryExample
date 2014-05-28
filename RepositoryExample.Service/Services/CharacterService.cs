using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using FluentValidation;
using RepositoryExample.Entity;

namespace RepositoryExample.Service
{
    public class CharacterService : ICharacterService
    {
        private readonly IDbSet<Character> m_characters;

        public CharacterService(IDbSet<Character> characters)
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

            return m_characters.Add(character);
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
        public IReadOnlyCollection<Character> GetAll()
        {
            return m_characters.OrderByDescending(m => m.Active)
                               .ThenBy(m => m.Name)
                               .ToList().AsReadOnly();
        }

        /// <summary>
        /// Retireve all Characters for the given Account
        /// </summary>
        public IReadOnlyCollection<Character> GetByAccount(Account account)
        {
            return m_characters.Where(m => m.Account.AccountId == account.AccountId)
                               .OrderByDescending(m => m.Active)
                               .ThenBy(m => m.Name)
                               .ToList().AsReadOnly();
        }

        #endregion
    }
}
