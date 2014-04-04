using System;
using System.Data.Entity.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation;
using RepositoryExample.Entity;
using RepositoryExample.Service;

namespace RepositoryExample.Test
{
    [TestClass]
    public class CharacterServiceTests
    {
        #region Tests

        [TestMethod]
        public void CanInsert()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test",
                    Race = CharacterRace.Human,
                    Class = CharacterClass.Warrior,
                    Faction = CharacterFaction.Alliance,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsTrue(session.SaveChanges());
            }
        }

        [TestMethod]
        public void CanGetAll()
        {
            using (var session = new WarcraftSession())
            {
                Assert.IsTrue(session.Characters.GetAll().Count == 1);
            }
        }

        [TestMethod]
        public void CanGetByName()
        {
            using (var session = new WarcraftSession())
            {
                Assert.IsNotNull(session.Characters.GetByName("test"));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CannotInsertOpposingFaction()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test2",
                    Race = CharacterRace.Orc,
                    Class = CharacterClass.Warrior,
                    Faction = CharacterFaction.Horde,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsFalse(session.SaveChanges());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CannotInsertBloodElfWarrior()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test2",
                    Race = CharacterRace.BloodElf,
                    Class = CharacterClass.Warrior,
                    Faction = CharacterFaction.Horde,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsFalse(session.SaveChanges());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CannotInsertHordeHuman()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test2",
                    Race = CharacterRace.Human,
                    Class = CharacterClass.Warrior,
                    Faction = CharacterFaction.Horde,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsFalse(session.SaveChanges());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CannotInsertAllianceOrc()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test2",
                    Race = CharacterRace.Orc,
                    Class = CharacterClass.Warrior,
                    Faction = CharacterFaction.Alliance,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsFalse(session.SaveChanges());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CannotInsertGnomeDruid()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test2",
                    Race = CharacterRace.Gnome,
                    Class = CharacterClass.Druid,
                    Faction = CharacterFaction.Alliance,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsFalse(session.SaveChanges());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CannotInsertDeathKnight()
        {
            using (var session = new WarcraftSession())
            {
                var character = session.Characters.Insert(new Character
                {
                    Name = "test2",
                    Race = CharacterRace.Gnome,
                    Class = CharacterClass.DeathKnight,
                    Faction = CharacterFaction.Alliance,
                    Account = session.Accounts.GetByEmail("test@test.com")
                });

                Assert.IsNotNull(character);
                Assert.IsFalse(session.SaveChanges());
            }
        }

        #endregion
    }
}
