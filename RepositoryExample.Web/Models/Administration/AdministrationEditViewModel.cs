using System.ComponentModel.DataAnnotations;
using RepositoryExample.Entity;

namespace RepositoryExample.Web.Models
{
    public class AdministrationEditViewModel
    {
        #region Properties

        [Required, Range(1, 85)]
        public int Level { get; set; }

        public Character Character { get; set; }

        #endregion
    }
}