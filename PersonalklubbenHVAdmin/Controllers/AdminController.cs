using Newtonsoft.Json;
using PersonalklubbenHVAdmin.Models;
using PersonalklubbenHVAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalklubbenHVAdmin.Controllers
{
    public class AdminController : Controller
    {
        List<Admins> admins = new List<Admins>();
        Admins sessionObjekt = new Admins();

        // GET: Admin
        public ActionResult AdminList()
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
        public ActionResult CreateAdmin()
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
        
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Medlems", "CreateMember"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAdmin(Admins nyMedlem)
        {
            sessionObjekt = (Admins)Session["admin"];

            if (Session["admin"] == null)
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            else
            {
                ViewBag.Username = "Inloggad som: " + sessionObjekt.Förnamn;
                sessionObjekt.Förnamn = string.Concat(sessionObjekt.Förnamn.Where(c => !char.IsWhiteSpace(c)));
                sessionObjekt.Efternamn = string.Concat(sessionObjekt.Efternamn.Where(c => !char.IsWhiteSpace(c)));
               // newMember.SkapadAv = "Medlemskap lades till av: " + sessionObjekt.Förnamn + " " + sessionObjekt.Efternamn + " den: " + DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Felmeddelande", "Formuläret felaktigt ifyllt!");
                return View();
            }
      
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");

                    var myContent = JsonConvert.SerializeObject(nyMedlem);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync("PhersonalklubbenREST/api/Admins", byteContent).Result;
                    string data = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Användare tillagd!");
                        return RedirectToAction("AdminList");

                    }
                    else
                    {
                        try
                        {
                            ModelState.AddModelError("Felmeddelande", "Allt fylldes inte i korrekt");
                            return View();
                        }
                        catch (Exception ex)
                        {
                            return View("Error", new HandleErrorInfo(ex, "Medlems", "CreateAdmin"));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Medlems", "CreateAdmin"));
            }

        }
        public ActionResult ShowAdminProfile(int ID)
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
            try
            {
                admins = GetAdmins();
                Admins admin = new Admins();
                admin = (from x in admins
                          where x.ID == ID
                          select x).FirstOrDefault();
                return View(admin);

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("Error", new HandleErrorInfo(ex, "Medlems", "ShowProfile"));
            }
        }
    }
}