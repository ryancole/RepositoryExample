using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using FluentValidation;
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
        /// Commit pending changes
        /// </summary>
        public override int SaveChanges()
        {
            return base.SaveChanges();
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

        /// <summary>
        /// initialize entity validators
        /// </summary>
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            // validate entity using schema rules
            var errors = base.ValidateEntity(entityEntry, items);

            // fail out early if there are already errors
            if (errors.ValidationErrors.Count > 0)
                return errors;

            // only continue with custom validation on insert or edit
            if (entityEntry.State != EntityState.Added && entityEntry.State != EntityState.Modified)
                return errors;

            // format the validation ruleset identifier
            var ruleset = entityEntry.State == EntityState.Added ? "default,insert" : "default";

            if (entityEntry.Entity is Account)
            {
                // cast the entity
                var account = entityEntry.Entity as Account;

                // initialize the validator
                var validator = new AccountValidator(GetDbSet<Account>());

                // collect any validation errors
                foreach (var error in validator.Validate(account, ruleSet: ruleset).Errors)
                {
                    errors.ValidationErrors.Add(new DbValidationError(error.PropertyName, error.ErrorMessage));
                }
            }
            else if (entityEntry.Entity is Character)
            {
                // cast the entity
                var account = entityEntry.Entity as Character;

                // initialize the validator
                var validator = new CharacterValidator(GetDbSet<Character>());

                // collect any validation errors
                foreach (var error in validator.Validate(account, ruleSet: ruleset).Errors)
                {
                    errors.ValidationErrors.Add(new DbValidationError(error.PropertyName, error.ErrorMessage));
                }
            }

            return errors;
        }

        #endregion
    }
}