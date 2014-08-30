using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RepositoryExample.Entity;

namespace RepositoryExample.Data
{
    internal class CharacterBindings : EntityTypeConfiguration<Character>
    {
        public CharacterBindings()
        {
            // name schema bindings
            Property(m => m.Name).IsRequired().HasMaxLength(12);

            // race schema bindings
            Property(m => m.Race).IsRequired();

            // class schema bindings
            Property(m => m.Class).IsRequired();

            // level schema bindings
            Property(m => m.Level).IsRequired();

            // faction schema bindings
            Property(m => m.Faction).IsRequired();
        }
    }
}
