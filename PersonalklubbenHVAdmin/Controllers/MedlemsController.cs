using Newtonsoft.Json;
using PersonalklubbenHVAdmin.Models;
using PersonalklubbenHVAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalklubbenHVAdmin.Controllers
{
    // [Authorize]

    public class MedlemsController : Controller
    {
        List<Medlem> memberList = new List<Medlem>();
        Admins sessionObjekt = new Admins();

        // GET: Medlems
        public ActionResult MedlemsIndex()
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
                memberList = ShowMembers();
                List<Medlem> activeMembers = new List<Medlem>();
                foreach (var item in memberList)
                {
                   
                        if (item.GiltighetsÅr.Year == DateTime.Now.Year || item.GiltighetsÅr.Year == DateTime.Now.Year + 1)
                        {
                             activeMembers.Add(item);
                        }
                    
                }

                int amount = activeMembers.Count();
                ViewBag.TotalMembers = amount;

                return View(activeMembers);

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("Error", new HandleErrorInfo(ex, "Medlems", "MedlemsIndex"));
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
                return View("Error", new HandleErrorInfo(ex, "Medlems", "ShowProfile"));
            }
        }
        public ActionResult TotalMembers()
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
                memberList = ShowMembers();
                int amount = memberList.Count();
                ViewBag.TotalMembers = amount;
                return View(memberList);

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("Error", new HandleErrorInfo(ex, "Medlems", "TotalMembers"));
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
            sessionObjekt = (Admins)Session["admin"];

            if (Session["admin"] == null)
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            else
            {
                ViewBag.Username = "Inloggad som: " + sessionObjekt.Förnamn;

            }
            CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

            viewmodel = createMemberViewmodel();

            try
            {

                return View(viewmodel);

            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Medlems", "CreateMember"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateMember (CreateMemberViewmodel nyMedlem)
        {
            sessionObjekt = (Admins)Session["admin"];
            Medlem newMember = new Medlem();

            if (Session["admin"] == null)
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            else
            {
                ViewBag.Username = "Inloggad som: " + sessionObjekt.Förnamn;
                sessionObjekt.Förnamn = string.Concat(sessionObjekt.Förnamn.Where(c => !char.IsWhiteSpace(c)));
                sessionObjekt.Efternamn = string.Concat(sessionObjekt.Efternamn.Where(c => !char.IsWhiteSpace(c)));

                newMember.SkapadAv = "Medlemskap lades till av: " + sessionObjekt.Förnamn + " " + sessionObjekt.Efternamn + " den: " + DateTime.Now;

            }


            if (!ModelState.IsValid)
            {
                CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

                viewmodel = createMemberViewmodel();

                return View(new CreateMemberViewmodel { Institutions = viewmodel.Institutions, years = viewmodel.years });
            }

            newMember.Förnamn = nyMedlem.medlem.Förnamn;
            newMember.Efternamn = nyMedlem.medlem.Efternamn;
            newMember.Epostadress = nyMedlem.medlem.Epostadress;
            newMember.Telefonnummer = nyMedlem.medlem.Telefonnummer;
            newMember.RegistreringsDatum = DateTime.Now;

            string institution = "";
            DateTime validdate = DateTime.MinValue; // Only for declaration, will give the output 1/1/0001 12:00:00 AM so it atleast wont be usable as a valid user.

            foreach (var item in nyMedlem.Institutions)
            {
                institution = item;
            }
            foreach (var item in nyMedlem.years)
            {
                validdate = item;
            }
            newMember.Institution = institution;
            newMember.GiltighetsÅr = validdate;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");

                    var myContent = JsonConvert.SerializeObject(newMember);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync("PhersonalklubbenREST/api/Medlemmars", byteContent).Result;
                    string data = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Användare tillagd!");
                        ViewBag.Message = String.Format("Ny medlem tillagd!\n Registrerades den{0}.",  DateTime.Now.ToString());

                        return RedirectToAction("MedlemsIndex");

                    }
                    else
                    {

                        CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

                        viewmodel = createMemberViewmodel();

                        try
                        {
                            ModelState.AddModelError("Felmeddelande", data);
                            return View(new CreateMemberViewmodel { Institutions = viewmodel.Institutions, years = viewmodel.years });

                        }
                        catch (Exception ex)
                        {

                            return View("Error", new HandleErrorInfo(ex, "Medlems", "CreateMember"));
                        }
                       
                    }
                }

            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Medlems", "CreateMember"));
            }

        }
        public ActionResult UpdateMembership(int id)
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
            CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

            viewmodel = createMemberViewmodel();

            try
            {
                memberList = ShowMembers();
                Medlem medlem = new Medlem();
                medlem = getMemberByID(id);
                viewmodel.medlem = medlem;
                return View(viewmodel);

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("Error", new HandleErrorInfo(ex, "Medlems", "ShowProfile"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateMembership(CreateMemberViewmodel updatedMember)
        {
            sessionObjekt = (Admins)Session["admin"];
            Medlem newMember = new Medlem();

            if (Session["admin"] == null)
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            else
            {
                ViewBag.Username = "Inloggad som: " + sessionObjekt.Förnamn;
                sessionObjekt.Förnamn = string.Concat(sessionObjekt.Förnamn.Where(c => !char.IsWhiteSpace(c)));
                sessionObjekt.Efternamn = string.Concat(sessionObjekt.Efternamn.Where(c => !char.IsWhiteSpace(c)));

                newMember.SkapadAv ="Medlemskap uppdataterades av: " + sessionObjekt.Förnamn + " " + sessionObjekt.Efternamn + " den: " + DateTime.Now ;

            }
            foreach (var item in updatedMember.Institutions)
            {
                updatedMember.medlem.Institution = item;
            }
            foreach (var item in updatedMember.years)
            {
                updatedMember.medlem.GiltighetsÅr = item;
            }

           
            if (!ModelState.IsValid)
            {
                CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

                viewmodel = createMemberViewmodel();

                memberList = ShowMembers();
                Medlem medlem = new Medlem();
                medlem = getMemberByID(updatedMember.medlem.ID);
                viewmodel.medlem = medlem;

                return View(new CreateMemberViewmodel { Institutions = viewmodel.Institutions, years = viewmodel.years, medlem = medlem });
            }

            newMember.ID = updatedMember.medlem.ID;
            newMember.Förnamn = updatedMember.medlem.Förnamn;
            newMember.Efternamn = updatedMember.medlem.Efternamn;
            newMember.Epostadress = updatedMember.medlem.Epostadress;
            newMember.Telefonnummer = updatedMember.medlem.Telefonnummer;
            newMember.RegistreringsDatum = updatedMember.medlem.RegistreringsDatum;
            newMember.Institution = updatedMember.medlem.Institution;
            newMember.GiltighetsÅr = updatedMember.medlem.GiltighetsÅr;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");

                    var myContent = JsonConvert.SerializeObject(newMember);
                    var buffer = Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync("PhersonalklubbenREST/api/UppdateraMedlemsskap", byteContent).Result;
                    string data = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Användare tillagd!");
                        return RedirectToAction("MedlemsIndex");

                    }
                    else
                    {

                        CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

                        viewmodel = createMemberViewmodel();

                        try
                        {
                            ModelState.AddModelError("Felmeddelande", "Medlemsskapet kunde ej förnyas, var god försök igen senare");

                            memberList = ShowMembers();
                            Medlem medlem = new Medlem();
                            medlem = getMemberByID(updatedMember.medlem.ID);
                            viewmodel.medlem = medlem;

                            return View(new CreateMemberViewmodel { Institutions = viewmodel.Institutions, years = viewmodel.years, medlem = medlem });
                        }
                        catch (Exception ex)
                        {

                            return View("Error", new HandleErrorInfo(ex, "Medlems", "TotalMembers"));
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Medlems", "TotalMembers"));
            }
        }

        public ActionResult DeleteMember(int id)
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
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://193.10.202.76/");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.DeleteAsync("/PhersonalklubbenREST/api/Medlemmars/" + id).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    Nyheter data = JsonConvert.DeserializeObject<Nyheter>(stringData);
                    if (response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("Felmeddelande", "Medlem raderad!");
                        return RedirectToAction("MedlemsIndex");
                    }
                    else
                    {
                        ModelState.AddModelError("Felmeddelande", "Det gick inte ta bort medlemmen. Försök senare igen.");
                        return RedirectToAction("MedlemsIndex");
                    }
                }

            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("Error", new HandleErrorInfo(ex, "Medlems", "DeleteMember"));

            }
        }
        public CreateMemberViewmodel createMemberViewmodel()
        {
            CreateMemberViewmodel viewmodel = new CreateMemberViewmodel();

            List<string> names = new List<string>();

            names.Add("Ekonomi & IT (EI)");
            names.Add("Ingenjörsvetenskap (IV)");
            names.Add("Institutionen för hälsovetenskap (IH)");
            names.Add("Individ och samhälle (IoS)");
            names.Add("Förvaltning (Forv)");
            names.Add("Studentstöd och bibliotek (SoB)");
            names.Add("Rektor (R)");

            viewmodel.Institutions = names;

            var list = new List<DateTime>();
            list.Add(new DateTime(DateTime.Now.Year, 12, 31));
            list.Add(new DateTime(DateTime.Now.Year + 1, 12, 31));
            list.Add(new DateTime(DateTime.Now.Year + 2, 12, 31));

            viewmodel.years = list;

            return viewmodel;
        }
        public Medlem getMemberByID (int id)
        {
            memberList = ShowMembers();
            Medlem medlem = new Medlem();
            medlem = (from x in memberList
                      where x.ID == id
                      select x).FirstOrDefault();

            medlem.Förnamn = string.Concat(medlem.Förnamn.Where(c => !char.IsWhiteSpace(c)));
            medlem.Efternamn = string.Concat(medlem.Efternamn.Where(c => !char.IsWhiteSpace(c)));
            medlem.Telefonnummer = string.Concat(medlem.Telefonnummer.Where(c => !char.IsWhiteSpace(c)));
            medlem.Epostadress = string.Concat(medlem.Epostadress.Where(c => !char.IsWhiteSpace(c)));

            return medlem;
        }

    }
    
}