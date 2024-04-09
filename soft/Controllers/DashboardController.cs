using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Security.Claims;

namespace soft.Controllers
{
    //[Authorize]
    public class DashboardController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        public DashboardController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        public IActionResult Index()  
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Membre> membres = new List<Membre>();
            List<Paiement> list = new List<Paiement>();
            List<Don> dons= new List<Don>();
            List<Assistance> assists= new List<Assistance>();

           
            try
            {
                HttpResponseMessage res0 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre").Result;
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Paiement").Result;
                HttpResponseMessage res1 = _httpClient.GetAsync(_httpClient.BaseAddress + "/v1/Don").Result;
                HttpResponseMessage res2 = _httpClient.GetAsync(_httpClient.BaseAddress + "/Demande").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data0 = res0.Content.ReadAsStringAsync().Result;
                    string data = res.Content.ReadAsStringAsync().Result;
                    string data1 = res1.Content.ReadAsStringAsync().Result;
                    string data2 = res2.Content.ReadAsStringAsync().Result;
                    membres=JsonConvert.DeserializeObject<List<Membre>>(data0);
                    list = JsonConvert.DeserializeObject<List<Paiement>>(data);
                    dons=JsonConvert.DeserializeObject<List<Don>>(data1);
                    assists=JsonConvert.DeserializeObject<List<Assistance>>(data2);

                    //  somme montant adhesion
                    int? somAdhesion = membres.Sum(k => k.MontantAdhesion);
                    ViewBag.SumAdhesion = somAdhesion.ToString();

                    //  somme cotisation
                    int somCotisation = list.Sum(k => k.Montant); 
                    ViewBag.SumCotisation= somCotisation.ToString();

                    // Montant total don
                    int totaldon=dons.Sum(d => d.Montant);
                    ViewBag.totaldon = totaldon.ToString();

                    // Montant total assistance
                    int montantAssistance = assists.Sum(k => k.Montant);
                    ViewBag.totalAssist= montantAssistance.ToString();

                    // revenu
                    int? revenu = (somCotisation + totaldon + somAdhesion) - montantAssistance;
                    ViewBag.revenu = revenu.ToString(); 

                    ViewBag.CotisationChartData = list
                                                        .Where(c=>c.Periodicite.annee==DateTime.Today.Year.ToString())
                                                        .GroupBy(j=>j.Periodicite.Id)
                                                        .Select(k => new
                                                        {
                                                            periode = k.First().Periodicite.mois + " " + k.First().Periodicite.annee,
                                                            montant=k.Sum(s=>s.Montant),
                                                            formatMontant=k.Sum(s=>s.Montant).ToString()+"F cfa",
                                                        })
                                                        .OrderByDescending(m=>m.montant)
                                                        .ToList();

                    // recent cotisation
                    ViewBag.CotisationsRecent = list
                                                .OrderByDescending(c=>c.datePaiement.ToString("dd-MM-yyyy"))
                                                .Take(5)
                                                .ToList();
                }
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }
          
            return View();
        }
        [HttpGet]
        public IActionResult logOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
