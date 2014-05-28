using System.Linq;
using System.Collections.Generic;
using RepositoryExample.Entity;

namespace RepositoryExample.Web.Models
{
    public class CharacterIndexViewModel
    {
        #region Properties

        public Account Account { get; set; }

        public IReadOnlyCollection<Character> Characters
        {
            get
            {
                return Account.Characters.ToList().AsReadOnly();
            }
        }

        #endregion
    }
}