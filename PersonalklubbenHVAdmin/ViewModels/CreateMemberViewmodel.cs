using PersonalklubbenHVAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalklubbenHVAdmin.ViewModels
{
    public class CreateMemberViewmodel
    {
        public List<DateTime> years { get; set; }
        public List<string> Institutions { get; set; }

        public Medlem medlem { get; set; }

     
         

    }
}