using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalklubbenHVAdmin.Models
{
    public class Medlemskonto
    {
        public int ID { get; set; }

        //[Required]
        //[StringLength(60, MinimumLength = 10)]
        ////[EmailAddress(ErrorMessage ="Ogiltig e-postadress")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+([-.]\w+)*@hv.se$",
        //                                        ErrorMessage = "E-postadressen har fel format")]
        public string Epost { get; set; }

        [Required]
        [StringLength(100)]
        public string Lösenord { get; set; }

        public int MedlemsID { get; set; }

        public virtual Medlem Medlemmar { get; set; }
    }
}