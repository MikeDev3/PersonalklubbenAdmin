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
                client.BaseAddress = new Uri("http://193.10.202.76/");

                var myContent = JsonConvert.SerializeObject(admin);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("api/AdminLogin/PostAdmin", byteContent).Result;

                string data = await result.Content.ReadAsStringAsync();
                Session["Username"] = "Admin - " + data;

                if (result.IsSuccessStatusCode)
                {
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
    }
}