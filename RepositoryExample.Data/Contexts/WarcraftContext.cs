using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using RepositoryExample.Entity;

namespace RepositoryExample.Data
{
    public class WarcraftContext : DbContext, IWarcraftContext
    {
        #region Methods

        /// <summary>
        /// get a db set for an entity type
        /// </summary>
        public IDbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// context entity bindings
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountBindings());
            modelBuilder.Configurations.Add(new CharacterBindings());

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}