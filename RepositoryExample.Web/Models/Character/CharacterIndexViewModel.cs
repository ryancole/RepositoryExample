using System.Collections.Generic;
using RepositoryExample.Entity;

namespace RepositoryExample.Web.Models
{
    public class CharacterIndexViewModel
    {
        #region Properties

        public Account Account { get; set; }

        public ICollection<Character> Characters { get; set; }

        #endregion
    }
}