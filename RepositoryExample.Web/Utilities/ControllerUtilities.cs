using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace RepositoryExample.Web.Utilities
{
    public class ControllerUtilities
    {
        #region Methods

        public static void MergeValidationErrors(ModelStateDictionary state, DbEntityValidationException errors)
        {
            foreach (var entity in errors.EntityValidationErrors)
            {
                foreach (var error in entity.ValidationErrors)
                {
                    state.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
        }

        #endregion
    }
}