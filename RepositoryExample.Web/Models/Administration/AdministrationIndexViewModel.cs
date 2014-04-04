using System.Collections.Generic;
using RepositoryExample.Entity;

namespace RepositoryExample.Web.Models
{
    public class AdministrationIndexViewModel
    {
        #region Properties

        public ICollection<Character> Characters { get; set; }

        #endregion
    }
}