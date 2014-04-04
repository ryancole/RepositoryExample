namespace RepositoryExample.Entity
{
    public class Character
    {
        #region Properties

        public int CharacterId { get; set; }

        public int Level { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public CharacterRace Race { get; set; }

        public CharacterClass Class { get; set; }

        public CharacterFaction Faction { get; set; }

        public virtual Account Account { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("{0}, Level {1} {2} {3}", Name, Level, Race, Class);
        }

        #endregion
    }

    public enum CharacterRace
    {
        Orc,
        Tauren,
        BloodElf,
        Human,
        Gnome,
        Worgen
    }

    public enum CharacterClass
    {
        Warrior,
        Druid,
        DeathKnight,
        Mage
    }

    public enum CharacterFaction
    {
        Alliance,
        Horde
    }
}
