using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSNexus.Model
{
    [Table("Characters")]
    public class Characters
    {
        //ID will be used as the primary key.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; } //unique ID for characters

        public string SteamID { get; set; }
        public byte Slot { get; set; }
        public string Name { get; set; }
        public byte Gender { get; set; } //id representation of gender
        public byte Race { get; set; } //id representation of race
        public string Flags { get; set; } //character sheet specific flags.
        public string Shortcuts { get; set; }
        public string Guild { get; set; } //guild name

        public short Kills { get; set; }
        public ulong Gold { get; set; }

        public string Skills { get; set; }

        public short Health { get; set; }
        public short Mana { get; set; }
        public string Equipped { get; set; }
        public string LeftHand { get; set; }
        public string Spells { get; set; }
        public string RightHand { get; set; }

        public string Spellbook { get; set; }
        public string HeavyBackpack { get; set; }
        public string BOH { get; set; }
        public string BOHLesser { get; set; }
        public string Sack { get; set; }
        public string BigSack { get; set; }
        public string Quiver { get; set; }
        public string XbowQuiver { get; set; }
        public string Sheaths { get; set; } //this will cover all sheaths by storing json.
    }
}
