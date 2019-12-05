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

        [StringLength(30)]
        public string Förnamn { get; set; }

        [StringLength(40)]
        public string Efternamn { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Epostadress { get; set; }

        [Required]
        [StringLength(100)]
        public string Lösenord { get; set; }
    }
}