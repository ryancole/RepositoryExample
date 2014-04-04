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
        /// validate updates to a Character
        /// </summary>
        public void Update(Character character)
        {
            // perform entity validation
            var validator = new CharacterValidator(m_characters);
            var validationresults = validator.Validate(character);

            if (!validationresults.IsValid)
                throw new ValidationException(validationresults.Errors);
        }

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

            // perform entity validation
            var validator = new CharacterValidator(m_characters);
            var validationresults = validator.Validate(character, ruleSet: "default,Insert");

            if (!validationresults.IsValid)
                throw new ValidationException(validationresults.Errors);

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
