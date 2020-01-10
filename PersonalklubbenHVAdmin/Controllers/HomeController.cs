using Newtonsoft.Json;
using PersonalklubbenHVAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Loinprojekt_admin.Controllers
{
    // [Authorize]

    public class HomeController : Controller
    {
        Admins sessionObjekt = new Admins();
        public ActionResult Index()
        {
            sessionObjekt = (Admins)Session["admin"];

            if (Session["admin"] == null)
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            else
            {
                ViewBag.Username = "Inloggad som: " + sessionObjekt.Förnamn;

            }

            int activeRow = 0;
            int newRegistrations = 0;
            int totalMembers = 0;

            StaticsticModel statistics = new StaticsticModel();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Medlemmars").Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<Medlem> data = JsonConvert.DeserializeObject<List<Medlem>>(stringData);

                    foreach (var item in data)
                    {
                        if (item.GiltighetsÅr.Year == DateTime.Now.Year || item.GiltighetsÅr.Year == DateTime.Now.Year + 1)
                        {
                            activeRow++;
                        }
                    }

                    totalMembers = data.Count();

                    statistics.ActiveRow = activeRow;
                    statistics.NewRegistrations = newRegistrations;
                    statistics.TotalMembersRow = totalMembers;
                    return View(statistics);
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }


        }
    }
}