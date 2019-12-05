using Newtonsoft.Json;
using PersonalklubbenHVAdmin.Models;
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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginIndex(Admins admin)
        {
           
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://193.10.202.76/PhersonalklubbenREST/");

                var myContent = JsonConvert.SerializeObject(admin);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = client.PostAsync("api/AdminLogin", byteContent).Result;

                string data = await result.Content.ReadAsStringAsync();
                Session["Username"] = "Admin - " + data;

                if (result.IsSuccessStatusCode)
                {
                    Admins session = new Admins();
                    session = GetAdminObject();
                    if (session != null)
                    {
                        Session["admin"] = session;

                    }

                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage(admin.Epostadress, false);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Inloggningen ej godkänd");
                    return View();

                }

            }
        }
        public Admins GetAdminObject()
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
                    Admins data = JsonConvert.DeserializeObject<Admins>(stringData);
                    return data;
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                Admins emptyAdmin = new Admins();
                return emptyAdmin;

            }

        }
        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("LoginIndex");
        }
    }
}