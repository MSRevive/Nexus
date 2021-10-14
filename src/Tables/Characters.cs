using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSNexus.Model
{
    [Table("Characters")]
    public class Characters
    {
        //ID will be used as the primary key.
        [Key]
        [Required]
        public string ID { get; set; }

        public short Slot { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }

        public short Kills { get; set; }
        public int Gold { get; set; }

        public short Health { get; set; }
        public short Mana { get; set; }
        public string Equipped { get; set; }
    }
}
