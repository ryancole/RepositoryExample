using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RepositoryExample.Entity;

namespace RepositoryExample.Data
{
    internal class AccountBindings : EntityTypeConfiguration<Account>
    {
        public AccountBindings()
        {
            ToTable("Accounts");

            Property(m => m.Email).IsRequired();
            Property(m => m.Password).IsRequired();
        }
    }
}
