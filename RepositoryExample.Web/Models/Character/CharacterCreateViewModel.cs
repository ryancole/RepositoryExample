using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RepositoryExample.Entity;

namespace RepositoryExample.Web.Models
{
    public class CharacterCreateViewModel
    {
        #region Properties

        [Required, StringLength(12, MinimumLength = 2)]
        [Display(Name = "Name", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Race", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public CharacterRace Race { get; set; }

        [Required]
        [Display(Name = "Class", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public CharacterClass Class { get; set; }

        [Required]
        [Display(Name = "Faction", ResourceType = typeof(RepositoryExample.Resources.Localization))]
        public CharacterFaction Faction { get; set; }

        public ICollection<SelectListItem> CharacterFactions
        {
            get
            {
                var factions = new List<SelectListItem>();

                foreach (var item in Enum.GetValues(typeof(CharacterFaction)))
                {
                    factions.Add(new SelectListItem
                    {
                        Text = Enum.GetName(typeof(CharacterFaction), item),
                        Value = Convert.ToString(item)
                    });
                }

                return factions;
            }
        }

        public ICollection<SelectListItem> CharacterRaces
        {
            get
            {
                var races = new List<SelectListItem>();

                foreach (var item in Enum.GetValues(typeof(CharacterRace)))
                {
                    races.Add(new SelectListItem
                    {
                        Text = Enum.GetName(typeof(CharacterRace), item),
                        Value = Convert.ToString(item)
                    });
                }

                return races;
            }
        }

        public ICollection<SelectListItem> CharacterClasses
        {
            get
            {
                var classes = new List<SelectListItem>();

                foreach (var item in Enum.GetValues(typeof(CharacterClass)))
                {
                    classes.Add(new SelectListItem
                    {
                        Text = Enum.GetName(typeof(CharacterClass), item),
                        Value = Convert.ToString(item)
                    });
                }

                return classes;
            }
        }

        #endregion
    }
}