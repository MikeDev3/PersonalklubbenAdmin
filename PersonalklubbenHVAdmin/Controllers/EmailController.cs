using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalklubbenHVAdmin.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult SendMail()
        {
            return View();
        }
    }
}