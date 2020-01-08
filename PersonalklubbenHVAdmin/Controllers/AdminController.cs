using Newtonsoft.Json;
using PersonalklubbenHVAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PersonalklubbenHVAdmin.Controllers
{
    public class AdminController : Controller
    {
        List<Admins> admins = new List<Admins>();
        // GET: Admin
        public ActionResult AdminList()
        {
            admins = GetAdmins();
            int amount = admins.Count();
            ViewBag.TotalMembers = amount;
            return View(admins);
        }
        public ActionResult DeleteAdmin()
        {
            return View();
        }
        public List<Admins> GetAdmins()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Admins").Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<Admins> data = JsonConvert.DeserializeObject<List<Admins>>(stringData);
                    return data;
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                List<Admins> emptyList = new List<Admins>();
                return emptyList;

            }

        }
    }
}