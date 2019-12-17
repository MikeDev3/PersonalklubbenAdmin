using Newtonsoft.Json;
using PersonalklubbenHVAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;

namespace PersonalklubbenHVAdmin.Controllers
{
    // [Authorize]

    public class NyheterController : Controller
    {
        // GET: Nyheter
        public ActionResult NyheterIndex()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Nyheters").Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<Nyheter> data = JsonConvert.DeserializeObject<List<Nyheter>>(stringData);
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
        public ActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNews(Nyheter nyhet)
        {
               try
                {
                     nyhet.PubliceringsDatum = DateTime.Now;
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://193.10.202.76/");

                        var myContent = JsonConvert.SerializeObject(nyhet);
                        var buffer = Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var result = client.PostAsync("PhersonalklubbenREST/api/Nyheters", byteContent).Result;
                        string data = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            ModelState.AddModelError("Felmeddelande", "Nyhet publicerad");
                            return RedirectToAction("NyheterIndex");
                        }
                        else
                        {
                            ModelState.AddModelError("Felmeddelande", "Något gick fel");
                            return RedirectToAction("CreateNews");
                        }
                    }

                }
                catch (Exception)
                {

                    ModelState.AddModelError("Felmeddelande", "Denna användare kan inte hittas.");
                    return RedirectToAction("NewRegistrations");
                }
            
        }

        public ActionResult DeleteNewsPost(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.DeleteAsync("/PhersonalklubbenREST/api/Nyheters/" + id).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    Nyheter data = JsonConvert.DeserializeObject<Nyheter>(stringData);
                    if (response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Nyhet raderad!");
                        return RedirectToAction("NyheterIndex");
                    }
                    else
                    {
                        ModelState.AddModelError("Felmeddelande", "Det gick inte ta bort nyheten. Försök senare igen.");
                        return RedirectToAction("NyheterIndex");
                    }
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return RedirectToAction("NyheterIndex");

            }
        }

        public ActionResult EditNews(int id)
        {
            Nyheter nyheter = new Nyheter();
            nyheter = GetNewsById(id);

            return View(nyheter);
        }
        [HttpPost]
        public async Task<ActionResult> EditNews(Nyheter updatedNews)
        {

            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        //  client.BaseAddress = new Uri("http://localhost:61607");
            //        string URL = @"http://193.10.202.76/api/Nyheters/" + updatedNews.ID;

            //        var myContent = JsonConvert.SerializeObject(updatedNews);
            //        var buffer = Encoding.UTF8.GetBytes(myContent);
            //        var byteContent = new ByteArrayContent(buffer);
            //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //        var result = client.PutAsync(URL, byteContent).Result;

            //        string data = await result.Content.ReadAsStringAsync();

            //        if (result.IsSuccessStatusCode)
            //        {
            //            ModelState.AddModelError("Felmeddelande", "Nyhet ändrad");
            //            return RedirectToAction("NyheterIndex");                       
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("Felmeddelande", "Något gick fel");
            //            return RedirectToAction("NyheterIndex");
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine(ex.ToString());
            //        return RedirectToAction("NyheterIndex");

            //    }
            //}

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");

                    var myContent = JsonConvert.SerializeObject(updatedNews);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PutAsync("PhersonalklubbenREST/api/Nyheters", byteContent).Result;
                    string data = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Nyhet ändrad");
                        return RedirectToAction("NyheterIndex");
                    }
                    else
                    {
                        ModelState.AddModelError("Felmeddelande", "Något gick fel");
                        return RedirectToAction("NyheterIndex");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Felmeddelande", "Denna användare kan inte hittas.");
                return RedirectToAction("NyheterIndex");
            }
        }

        public Nyheter GetNewsById (int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Nyheters/" + id).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    Nyheter data = JsonConvert.DeserializeObject<Nyheter>(stringData);
                    return data;
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                Nyheter emptyNews = new Nyheter();
                return emptyNews;
            }
        }
    }
}