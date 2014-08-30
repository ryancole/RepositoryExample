using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RepositoryExample.Entity;

namespace RepositoryExample.Data
{
    internal class AccountBindings : EntityTypeConfiguration<Account>
    {
        public AccountBindings()
        {
            // email schema bindings
            Property(m => m.Email).IsRequired();

            // password schema bindings
            Property(m => m.Password).IsRequired();
        }
    }
}
