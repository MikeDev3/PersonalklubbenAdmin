using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalklubbenHVAdmin.Models
{
    public class Medlem
    {
        public Medlem()
        {
            Medlemskonto = new HashSet<Medlemskonto>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Förnamn { get; set; }

        [Required]
        [StringLength(40)]
        public string Efternamn { get; set; }

        [Required]
        [StringLength(50)]
        public string Institution { get; set; }

        [Required]
        [StringLength(60)]
        public string Epostadress { get; set; }

        [Column(TypeName = "date")]
        public DateTime GiltighetsÅr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medlemskonto> Medlemskonto { get; set; }
    }
}