using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonalklubbenHVAdmin.Models
{
    public class Appliances
    {
        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(40)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        public string Institution { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidToDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegistreredDate { get; set; }
    }
}