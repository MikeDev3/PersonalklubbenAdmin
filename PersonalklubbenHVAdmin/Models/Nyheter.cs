using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalklubbenHVAdmin.Models
{
    public class Nyheter
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Rubrik { get; set; }

        [StringLength(100)]
        public string Underrubrik { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Beskrivning { get; set; }

        [Column(TypeName = "date")]
        public DateTime PubliceringsDatum { get; set; }
        [StringLength(200)]
        public string BildURL { get; set; }
        [StringLength(200)]
        public string Link1 { get; set; }
        [StringLength(200)]
        public string Link2 { get; set; }
    }
}
