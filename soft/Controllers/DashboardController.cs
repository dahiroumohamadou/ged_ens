using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ged.Models;

namespace ged.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        public DashboardController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            
        }
        public IActionResult Index()  
        {
            List<Doc> documents= new List<Doc>();
            try
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    documents=JsonConvert.DeserializeObject<List<Doc>>(data);
                    ViewBag.arretes=documents.Where(d=>d.TypeDoc=="ARR").Count();
                    ViewBag.pvs = documents.Where(d => d.TypeDoc == "PV").Count();
                    ViewBag.communiques = documents.Where(d => d.TypeDoc == "CRP").Count();
                    ViewBag.DossiersChartData=documents
                                                            //.Where(d => d.Created. == DateTime.Today.Year.ToString())
                                                            .GroupBy(j=>j.TypeDoc)
                                                            .Select(k=>new
                                                            {
                                                                type=k.First().TypeDoc,
                                                                nbeDossiers=k.Count()
                                                            })
                                                            .ToList();
                    ViewBag.DossiersChartDataSpline1 = documents
                                                .Where(d => d.Source=="ENS-YDE")
                                                .GroupBy(j => j.TypeDoc)
                                                .Select(k => new
                                                {
                                                    type = k.First().TypeDoc,
                                                    nbeDossiers = k.Count()
                                                })
                                                .ToList();
                    ViewBag.DossiersChartDataSpline2 = documents
                                               .Where(d => d.Source == "ENS-BAMBILI")
                                               .GroupBy(j => j.TypeDoc)
                                               .Select(k => new
                                               {
                                                   type = k.First().TypeDoc,
                                                   nbeDossiers = k.Count()
                                               })
                                               .ToList();
                    ViewBag.DossiersRecent = documents
                                              .OrderByDescending(d => d.Created)
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
            ViewBag.user = "";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
