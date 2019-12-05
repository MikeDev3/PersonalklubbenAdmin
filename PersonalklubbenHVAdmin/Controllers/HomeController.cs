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
        public ActionResult Index()
        {
            Admins sessionObjekt = (Admins)Session["admin"];

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
                return View();
            }


            return View();
        }


      
        //funkar
        public ActionResult ShowProfile(int id)
        {            
//            UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
//=======
//            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

//            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
//            try
//            {
//                // Anrop till webservicen
//                UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
//>>>>>>> Sessions klara, man måste starta från vyn "LoginPage" och logga in med kontot Admin/Admin

//            return View(client.GetUserByUserId(id));
            return View();
        }
        //funkar
        public ActionResult ActiveUsers()
        {
            //            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            //            return View(client.GetActiveUsers());
            //            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //            try
            //            {
            //                // Anrop till webservicen
            //                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //                if (client.GetActiveUsers() != null)
            //                {
            //                    // Anrop till servicens metod för att visa alla aktiva användare
            //                    return View(client.GetActiveUsers());

            //                }
            //                else
            //                {
            //                    ModelState.AddModelError("Felmeddelande", "It seems like there are no active users yet");
            //                    return View();
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            //            }
            //>>>>>>> Sessions klaraa, man måste starta från vyn "LoginPage" och logga in med kontot Admin/Admin
            return View();

        }
        //funkar
        public ActionResult Moderators()
        {

            //            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //            try
            //            {
            //                // Anrop till webservicen
            //                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //                if (client.GetModerators() != null)
            //                {
            //>>>>>>> Sessions klara, man måste starta från vyn "LoginPage" och logga in med kontot Admin/Admin

            //            return View(client.GetModerators());
            return View();

        }
        //funkar
        public ActionResult FlaggedErrands()
        {

            //            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            //            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //            try
            //            {
            //                // Anrop till webservicen
            //                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //                if (client.GetFlaggedUsers() != null)
            //                {
            //                    // Anrop till webservicens metod för att visa alla flaggade ärenden
            //                    return View(client.GetFlaggedUsers());
            //>>>>>>> Sessions klara, man måste starta från vyn "LoginPage" och logga in med kontot Admin/Admin

            //            return View(client.GetFlaggedUsers());

            return View();

        }
        //funkar
        public ActionResult BlockedUsers()
        {
            //            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            //            return View(client.GetBlockedUsers());

            //            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //            try
            //            {
            //                // Anrop till webservicen
            //                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //                if (client.GetBlockedUsers() != null)
            //                {
            //                    // Anrop till webservicens metod för att visa alla blockade användare
            //                    return View(client.GetBlockedUsers());
            //                }
            //                else
            //                {
            //                    ModelState.AddModelError("Felmeddelande", "No blocked users, good for us!");
            //                    return View();
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            //            }

            //>>>>>>> Sessions klara, man måste starta från vyn "LoginPage" och logga in med kontot Admin/Admin

            return View();

        }

        //funkar
        public ActionResult Delete(int id)
        {
            //UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
            //return View(client.GetUserByUserId(id));
            //Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //try
            //{
            //    // Anrop till webservicen
            //    UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
            //    if (client.GetUserByUserId(id) != null)
            //    {
            //        // Anrop till webservicens metod för att hitta en specifik användare och visa upp en vy utifrån detta
            //        return View(client.GetUserByUserId(id));
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("Felmeddelande", "Cant find this user");
            //        return View();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            //}

            return View();


        }
        //funkar
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            //LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //client.DeleteUser(id);
            //Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //try
            //{
            //    // Anrop till webservicen
            //    LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //    // Anrop till webservicens metod för att radera en användare, där vi skickar me med den specifika användarens Id
            //    client.DeleteUser(id);
            //    // När raderingen slutförs, återvänd till sidan som visar alla aktiva användare
            //    ModelState.AddModelError("Felmeddelande", "Konto raderat!");
            //    return RedirectToAction("ActiveUsers");

            //return RedirectToAction("ActiveUsers");
            return View();

        }
        //funkar
        public ActionResult AddPermission(int id)
        {
            //LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // client.AssignModeratorRole(id);
            // Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            // ViewBag.Username = "Inloggad som: " + sessionObjekt.username;

            // return RedirectToAction("ActiveUsers");
            return View();

        }
        //funkar
        public ActionResult DeletePermission(int id)
        {
            //LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //client.AssignUserRole(id);

            //return RedirectToAction("Moderators");
            //Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //try
            //{
            //    // Anrop till webservicen
            //    LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //    if (client.AssignUserRole(id) == true)
            //    {
            //        // Här ropas på webserivcens metod för att lägga till behörigheter, är det rätt?
            //        client.AssignUserRole(id);
            //        // När behörigheten är raderad, återvänd till sidan som visar alla moderatorer
            //        return RedirectToAction("Moderators");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("Felmeddelande", "The user is already a normal user");
            //        return View();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            //}
            return View();

        }
        //funkar
        public ActionResult Unflag(int id)
        {
            //LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //try
            //{
            //    // Anrop till webservicen
            //    LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //    if (client.AssignUserRole(id) == true)
            //    {
            //        // Anrop till webservicens metod för att ta bort flaggan från en användare, här skickar vi med Id:t för den specifika användaren
            //        client.UnflagUser(id);
            //        // När operationen är klar, återvänd till sidan som visar alla aktiva användare
            //        return RedirectToAction("ActiveUsers");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("Felmeddelande", "The user cant be unflagged");
            //        return View();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            //}

            //client.UnflagUser(id);
            //return RedirectToAction("ActiveUsers");

            return View();

        }


        //KVAR ATT GÖRA:

        /* public ActionResult Block(int id)
         {
             LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
             client.BlockUser(id);
             return RedirectToAction("ActiveUsers");
         }*/


        public ActionResult Contact()
        {
            //LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //client.GetAdmins();
            //return View();
            //  Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            //ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            //try
            //{
            //    // Anrop till webservicen
            //    LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            //    if (client.GetAdmins() != null)
            //    {

            //        // Anrop till webserivcens metod för att hämta alla admins
            //        return View(client.GetAdmins());
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("Felmeddelande", "It seems like there are no admins yet");
            //        return View();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            //}

            return View();

        }
        public ActionResult LogOut()
        {
            return View();
        }


        //public BookAFlight()
        //{
        //    this.InitializeComponent();
        //    ReadFlights();
        //}
        //protected async override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (e.Parameter != null)
        //    {
        //        try
        //        {
        //            string URL = @"http://localhost:61607/Travelers/" + e.Parameter;
        //            HttpClient httpClient = new HttpClient();
        //            HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                var travelerObjectFromAPI = JsonConvert.DeserializeObject<Travelers>(content);
        //                id = travelerObjectFromAPI.ID;
        //                loggedInTraveler = travelerObjectFromAPI;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //ToDo Give errormessage to user and possibly log error
        //            System.Diagnostics.Debug.WriteLine(ex.ToString());
        //        }

        //    }
        //}
        //public async void ReadFlights()
        //{
        //    var flightListFromAPI = new List<Flights>();
        //    var airportListFromAPI = new List<Airport>();

        //    try
        //    {

        //        string URL = @"http://localhost:61607/flights";
        //        HttpClient httpClient = new HttpClient();
        //        HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            flightListFromAPI = JsonConvert.DeserializeObject<List<Flights>>(content);

        //            //Databind the list
        //            //flightList.ItemsSource = flightListFromAPI;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //ToDo Give errormessage to user and possibly log error
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }
        //    try
        //    {
        //        string URLairport = @"http://localhost:61607/Airports";
        //        HttpClient httpClient = new HttpClient();
        //        HttpResponseMessage response2 = await httpClient.GetAsync(new Uri(URLairport));

        //        if (response2.IsSuccessStatusCode)
        //        {
        //            var content2 = await response2.Content.ReadAsStringAsync();
        //            airportListFromAPI = JsonConvert.DeserializeObject<List<Airport>>(content2);

        //            //Databind the list
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ToDo Give errormessage to user and possibly log error
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }

        //    List<Models.ViewModels.FlightAndAirportsModel> presentationList = new List<Models.ViewModels.FlightAndAirportsModel>();


        //    foreach (var item in flightListFromAPI)
        //    {
        //        Airport fromAirport = airportListFromAPI.Where(x => x.ID == item.FromAirport).FirstOrDefault();
        //        Airport toAirport = airportListFromAPI.Where(x => x.ID == item.ToAirport).FirstOrDefault();

        //        Models.ViewModels.FlightAndAirportsModel presentationObject = new Models.ViewModels.FlightAndAirportsModel
        //        {
        //            FlightID = item.ID,
        //            Price = item.Price,
        //            DateFrom = item.DateFrom,
        //            DateTo = item.DateTo,
        //            FromAirportName = fromAirport.AirportName + " - " + fromAirport.Country,
        //            ToAirportName = toAirport.AirportName + " - " + toAirport.Country,
        //            FromAirport = fromAirport.ID,
        //            ToAirport = toAirport.ID

        //        };

        //        presentationList.Add(presentationObject);
        //    }
        //    flightList.ItemsSource = presentationList;
        //    // flightList.SelectedIndex = -1;
        //}

        //public async System.Threading.Tasks.Task AddFlightAsync()
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();

        //        //We need to convert back from ViewModel AuthorPresentation to Author
        //        Models.ViewModels.FlightAndAirportsModel selectedFlight = (Models.ViewModels.FlightAndAirportsModel)flightList.SelectedItem; //Hämta det valda objektet

        //        Models.ViewModels.FlightAndAirportsModel selectedObject = flightList.SelectedItem as Models.ViewModels.FlightAndAirportsModel;


        //        var flightThatIsChosen = new Flights();
        //        try
        //        {
        //            string URL = @"http://localhost:61607/flights/" + selectedFlight.FlightID;
        //            HttpClient httpClient = new HttpClient();
        //            HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                flightThatIsChosen = JsonConvert.DeserializeObject<Flights>(content);

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            //ToDo Give errormessage to user and possibly log error
        //            System.Diagnostics.Debug.WriteLine(ex.ToString());
        //        }
        //        loggedInTraveler.Flights.Add(flightThatIsChosen);
        //        flightThatIsChosen.Travelers.Add(loggedInTraveler);


        //        try
        //        {
        //            string URL = @"http://localhost:61607/";
        //            HttpClient httpClient = new HttpClient();

        //            var myContent = JsonConvert.SerializeObject(flightThatIsChosen);
        //            var buffer = Encoding.UTF8.GetBytes(myContent);
        //            var byteContent = new ByteArrayContent(buffer);
        //            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //            var result = client.PostAsync(URL + "FlightBookings", byteContent).Result;

        //            if (result.IsSuccessStatusCode)
        //            {
        //                //GO to next page
        //                this.Frame.Navigate(typeof(LoggedInMainMenu), id);

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            System.Diagnostics.Debug.WriteLine(ex.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //ToDo Give errormessage to user and possibly log error
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }
        //}

        //public async System.Threading.Tasks.Task PostFlightAsync(Flights newBooking)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        try
        //        {
        //            client.BaseAddress = new Uri("http://localhost:61607");
        //            MediaTypeWithQualityHeaderValue content = new MediaTypeWithQualityHeaderValue("application/json");
        //            client.DefaultRequestHeaders.Accept.Add(content);
        //            // HTTP POST
        //            var message = new HttpRequestMessage(HttpMethod.Post, "FlightBookings/" + id + "/" + newBooking.ID);
        //            var result = await client.SendAsync(message);

        //            string resultContent = await result.Content.ReadAsStringAsync();


        //            if (result.IsSuccessStatusCode)
        //            {
        //                this.Frame.Navigate(typeof(LoggedInMainMenu), id);
        //                var dialog = new MessageDialog("Your flight has been booked");
        //                await dialog.ShowAsync();
        //            }
        //            else
        //            {
        //                var dialog = new MessageDialog(resultContent);
        //                await dialog.ShowAsync();
        //                this.Frame.Navigate(typeof(LoggedInMainMenu), id);

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            System.Diagnostics.Debug.WriteLine(ex.ToString());
        //        }
        //    }
        //}

        //private void GoToMainMenu(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(LoggedInMainMenu), id);

        //}

        //private async void FlightList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    HttpClient client = new HttpClient();

        //    //We need to convert back from ViewModel AuthorPresentation to Author
        //    Models.ViewModels.FlightAndAirportsModel selectedFlight = (Models.ViewModels.FlightAndAirportsModel)flightList.SelectedItem; //Hämta det valda objektet
        //    var flightThatIsChosen = new Flights();

        //    try
        //    {
        //        string URL = @"http://localhost:61607/flights/" + selectedFlight.FlightID;
        //        HttpClient httpClient = new HttpClient();
        //        HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            flightThatIsChosen = JsonConvert.DeserializeObject<Flights>(content);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //ToDo Give errormessage to user and possibly log error
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }
        //    // loggedInTraveler.Flights.Add(flightThatIsChosen);
        //    flightThatIsChosen.Travelers.Add(loggedInTraveler);

        //    await PostFlightAsync(flightThatIsChosen);
        //}


    }
}