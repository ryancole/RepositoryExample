using System.Collections.Generic;
using RepositoryExample.Entity;

namespace RepositoryExample.Web.Models
{
    public class AdministrationIndexViewModel
    {
        #region Properties

        public IReadOnlyCollection<Character> Characters { get; set; }

        #endregion
    }
}