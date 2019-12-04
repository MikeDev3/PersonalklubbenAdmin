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
    public class MedlemsController : Controller
    {
        // GET: Medlems
        public ActionResult MedlemsIndex()
        {
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
                    return View(data);
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }
        public ActionResult NewRegistrations()
        {
            return View();
        }

        public ActionResult ShowProfile(int ID)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Medlemmars/" + ID).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    Medlem data = JsonConvert.DeserializeObject<Medlem>(stringData);
                    return View(data);
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }
    }
}