using System.Linq;
using FluentValidation;
using RepositoryExample.Entity;
using RepositoryExample.Repository;

namespace RepositoryExample.Service
{
    internal class CharacterValidator : AbstractValidator<Character>
    {
        private readonly IRepository<Character> m_characters;

        public CharacterValidator(IRepository<Character> characters)
        {
            m_characters = characters;

            RuleSet("Insert", () =>
            {

                // name validation rules
                RuleFor(m => m.Name).NotEmpty()
                                    .Length(2, 12)
                                    .Must(NameIsAvailable).WithMessage("{0} is already taken", m => m.Name);

            });

            // level validation rules
            RuleFor(m => m.Level).NotEmpty()
                                 .InclusiveBetween(1, 85)
                                 .Must(AboveDeathKnightMinimum)
                                 .WithMessage("Death Knight may not go below level 55");

            // faction validation rules
            RuleFor(m => m.Faction).NotNull()
                                   .Must(FactionAllowsFaction)
                                   .WithMessage("{0} is not allowed because you have characters on other factions", m => m.Faction);

            // race validation rules
            RuleFor(m => m.Race).NotNull()
                                .Must(FactionAllowsRace)
                                .WithMessage("{0} is not allowed on {1}", x => x.Race, x => x.Faction);

            // class validation rules
            RuleFor(m => m.Class).NotNull()
                                 .Must(RaceAllowsClass).WithMessage("{0} cannot be a {1}", m => m.Race, m => m.Class)
                                 .Must(LevelAllowsDeathKnight).WithMessage("Death Knight requires at least one existing level 55 character");
        }

        #region Methods

        private bool AboveDeathKnightMinimum(Character character, int level)
        {
            if (character.Class == CharacterClass.DeathKnight && level < 55)
                return false;

            return true;
        }

        private bool NameIsAvailable(string name)
        {
            if (m_characters.Count(m => m.Name.ToLower() == name.ToLower()) > 0)
                return false;

            return true;
        }

        private bool FactionAllowsRace(Character character, CharacterRace race)
        {
            if (character.Faction == CharacterFaction.Alliance && (race == CharacterRace.Gnome ||
                                                                   race == CharacterRace.Human ||
                                                                   race == CharacterRace.Worgen))
                return true;

            if (character.Faction == CharacterFaction.Horde && (race == CharacterRace.BloodElf ||
                                                                race == CharacterRace.Orc ||
                                                                race == CharacterRace.Tauren))
                return true;

            return false;
        }

        private bool RaceAllowsClass(Character character, CharacterClass classs)
        {
            if (classs == CharacterClass.Druid && (character.Race != CharacterRace.Worgen &&
                                                   character.Race != CharacterRace.Tauren))
                return false;

            if (classs == CharacterClass.Warrior && character.Race == CharacterRace.BloodElf)
                return false;

            return true;
        }

        private bool LevelAllowsDeathKnight(Character character, CharacterClass classs)
        {
            if (classs != CharacterClass.DeathKnight)
                return true;

            if (m_characters.Count(m => m.Account.AccountId == character.Account.AccountId && m.Level >= 55) > 0)
                return true;

            return false;
        }

        private bool FactionAllowsFaction(Character character, CharacterFaction faction)
        {
            var characters = m_characters.Where(m => m.Account.AccountId == character.Account.AccountId);

            if (characters.All(m => m.Faction == faction))
                return true;

            return false;
        }

        #endregion
    }
}
