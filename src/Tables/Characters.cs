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
        public string Quickslots { get; set; }
        public string Quests { get; set; }
        public string Guild { get; set; } //guild/party name

        public short Kills { get; set; }
        public ulong Gold { get; set; }

        public string Skills { get; set; }
        public string Pets { get; set; }

        public short Health { get; set; }
        public short Mana { get; set; }
        public string Equipped { get; set; }
        public string LeftHand { get; set; }
        public string RightHand { get; set; }
        public string Spells { get; set; } //for learned spells

        public string Spellbook { get; set; } //for the spell scrolls.
        public string Bags { get; set; } //this will cover all the bags.
        public string Sheaths { get; set; } //this will cover all sheaths by storing json.
    }
}
