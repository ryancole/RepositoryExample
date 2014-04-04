using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using RepositoryExample.Entity;

namespace RepositoryExample.Data
{
    internal class CharacterBindings : EntityTypeConfiguration<Character>
    {
        public CharacterBindings()
        {
            ToTable("Characters");

            Property(m => m.Name).IsRequired().HasMaxLength(12);
            Property(m => m.Race).IsRequired();
            Property(m => m.Class).IsRequired();
            Property(m => m.Level).IsRequired();
            Property(m => m.Faction).IsRequired();
        }
    }
}
