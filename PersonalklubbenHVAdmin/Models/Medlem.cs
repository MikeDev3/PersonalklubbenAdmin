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

        [Required(ErrorMessage = "Please enter name")]
        //[RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        // ErrorMessage = "Characters are not allowed.")]
        public string Förnamn { get; set; }

        [Required]
        [StringLength(40)]
        public string Efternamn { get; set; }
        [Required]
        [StringLength(25)]
        public string Telefonnummer { get; set; }

        [Required]
        [StringLength(50)]
        public string Institution { get; set; }

        [Required]
        [StringLength(60)]
        public string Epostadress { get; set; }

        [Column(TypeName = "date")]
        public DateTime GiltighetsÅr { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegistreringsDatum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medlemskonto> Medlemskonto { get; set; }
    }
}