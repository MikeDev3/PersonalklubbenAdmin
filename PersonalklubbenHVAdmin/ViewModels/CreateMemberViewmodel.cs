using PersonalklubbenHVAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalklubbenHVAdmin.ViewModels
{
    public class CreateMemberViewmodel
    {
        public  List<DateTime> years = new List<DateTime>();
        public List<string> Institutions = new List<string>();

        public Medlem medlem = new Medlem();
         

    }
}