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
            List<Appliances> newMembers = new List<Appliances>();
            newMembers = ShowRegistrations();
            return View(newMembers);
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

        public ActionResult ApproveMembership (int id)
        {
            Appliances appliance = new Appliances();
            appliance = getAppliance(id);

            Medlem newMember = new Medlem();
            newMember.Förnamn = appliance.Firstname;
            newMember.Efternamn = appliance.Lastname;
            newMember.Epostadress = appliance.Email;
            newMember.GiltighetsÅr = appliance.ValidToDate;
            newMember.RegistreringsDatum = appliance.RegistreredDate;
            newMember.Telefonnummer = appliance.PhoneNumber;
            newMember.Institution = appliance.Institution;

            return View(newMember);

        }
        [HttpPost, ActionName("ApproveMembership")]
        [ValidateAntiForgeryToken]
        /*
         Metod för att bekräfta radering av en användare, här utförs således den faktiska raderingen.
         Metoden tar den specifika användarens Id som inparamter.
         */
        public async Task<ActionResult> ConfirmRegistration(Medlem nyMedlem)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");

                    var myContent = JsonConvert.SerializeObject(nyMedlem);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync("PhersonalklubbenREST/api/Medlemmars", byteContent).Result;
                    string data = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Medlem registrerad");
                        return RedirectToAction("MedlemsIndex");

                    }
                    else
                    {
                        ModelState.AddModelError("Felmeddelande", "Denna användare kan inte hittas.");
                        return RedirectToAction("NewRegistrations");
                    }
                }

            }
            catch (Exception)
            {

                ModelState.AddModelError("Felmeddelande", "Denna användare kan inte hittas.");
                return RedirectToAction("NewRegistrations");
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
        public List<Appliances> ShowRegistrations()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Appliances").Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<Appliances> data = JsonConvert.DeserializeObject<List<Appliances>>(stringData);
                    return data;
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                List<Appliances> emptyList = new List<Appliances>();
                return emptyList;

            }
        }
        public Appliances getAppliance (int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync("/PhersonalklubbenREST/api/Appliances/" + id).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    Appliances data = JsonConvert.DeserializeObject<Appliances>(stringData);
                    return data;
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                Appliances emptyObject = new Appliances();
                return emptyObject;

            }
        }
        public ActionResult CreateMember()
        {
            CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();
            viewmodel.Institutions.Add("Ekonomi & IT (EI)");
            viewmodel.Institutions.Add("Ingenjörsvetenskap(IV)");
            viewmodel.Institutions.Add("Institutionen för hälsovetenskap(IH)");
            viewmodel.Institutions.Add("Individ och samhälle(IoS)");
            viewmodel.Institutions.Add("Förvaltning(Forv)");
            viewmodel.Institutions.Add("Studentstöd och bibliotek(SoB)");
            viewmodel.Institutions.Add("Rektor(R)");

            const int numberOfYears = 3;
            var startYear = DateTime.Now.Year;
            var endYear = startYear + numberOfYears;

            var yearList = new List<SelectListItem>();
            for (var i = startYear; i < endYear; i++)
            {
                yearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            var list = new List<DateTime>();
            list.Add(new DateTime(DateTime.Now.Year, 12, 31));
            list.Add(new DateTime(DateTime.Now.Year + 1, 12, 31));
            list.Add(new DateTime(DateTime.Now.Year + 2, 12, 31));

            viewmodel.years = list;

           
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult CreateMember (Medlem nyMedlem)
        {
            return View();
        }
    }
}