using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalklubbenHVAdmin.Models
{
    public class Admins
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Förnamn { get; set; }

        [Required]
        [StringLength(40)]
        public string Efternamn { get; set; }

        [Required]
        [StringLength(60)]
        public string Epostadress { get; set; }

        [Required]
        [StringLength(100)]
        public string Lösenord { get; set; }
    }
}