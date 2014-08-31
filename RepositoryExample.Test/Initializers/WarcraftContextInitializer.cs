using System;
using System.Data.Entity;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RepositoryExample.Data;
using RepositoryExample.Entity;

namespace RepositoryExample.Test
{
    public class WarcraftContextInitializer : DropCreateDatabaseAlways<WarcraftContext>
    {
        #region Methods

        protected override void Seed(WarcraftContext context)
        {
            var accounts = context.GetDbSet<Account>();
            var characters = context.GetDbSet<Character>();

            var account = accounts.Add(new Account
            {
                Email = "test@test.com",
                IsAdmin = true,
                Password = "098f6bcd4621d373cade4e832627b4f6"
            });

            var character = characters.Add(new Character
            {
                Account = account,
                Name = "test",
                Class = CharacterClass.Druid,
                Faction = CharacterFaction.Alliance,
                Race = CharacterRace.Worgen
            });

            context.SaveChanges();
        }

        #endregion
    }
}
