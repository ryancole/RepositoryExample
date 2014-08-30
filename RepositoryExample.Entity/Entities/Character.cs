namespace RepositoryExample.Entity
{
    public class Character
    {
        public Character()
        {
            Level = 1;
            Active = true;
        }

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
        #region Values

        Orc,
        Tauren,
        BloodElf,
        Human,
        Gnome,
        Worgen

        #endregion
    }

    public enum CharacterClass
    {
        #region Values

        Warrior,
        Druid,
        DeathKnight,
        Mage

        #endregion
    }

    public enum CharacterFaction
    {
        #region Values

        Alliance,
        Horde

        #endregion
    }
}
