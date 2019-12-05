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
    // [Authorize]

    public class MedlemsController : Controller
    {
        List<Medlem> memberList = new List<Medlem>();
        
        // GET: Medlems
        public ActionResult MedlemsIndex()
        {
            try
            {
                memberList = ShowMembers();
                List<Medlem> activeMembers = new List<Medlem>();
                foreach (var item in memberList)
                {
                   
                        if (item.GiltighetsÅr.Year == DateTime.Now.Year || item.GiltighetsÅr.Year == DateTime.Now.Year + 1)
                        {
                             activeMembers.Add(item);
                        }
                    
                }
                return View(activeMembers);

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
                memberList = ShowMembers();
                Medlem medlem = new Medlem();
                medlem = (from x in memberList
                          where x.ID == ID
                          select x).FirstOrDefault();
                return View(medlem);

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }
        public ActionResult TotalMembers()
        {
            try
            {
                memberList = ShowMembers();
                return View(memberList);

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }
        public List<Medlem> ShowMembers()
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
                    return data;
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                List<Medlem> emptyList = new List<Medlem>();
                return emptyList;

            }

        }
    }
}