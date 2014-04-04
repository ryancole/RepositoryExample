using System.Collections.Generic;

namespace RepositoryExample.Entity
{
    public class Account
    {
        public Account()
        {
            this.Characters = new List<Character>();
        }

        #region Properties

        public int AccountId { get; set; }

        public bool IsAdmin { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        #endregion
    }
}
