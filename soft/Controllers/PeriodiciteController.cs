using soft.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace e_Soft.Controllers
{
    //[Authorize]
    public class PeriodiciteController : Controller
    {
        Uri baseAdresse = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        // GET: PeriodiciteController

        public PeriodiciteController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdresse;
        }
        [HttpGet]
        public ActionResult Index()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Periodicite> periodes = new List<Periodicite>();
            try
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Periodicite").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    periodes = JsonConvert.DeserializeObject<List<Periodicite>>(data);
                    ViewBag.DataSource = periodes;

                }

            }
            catch (Exception ex)
            {

            }
            return View(periodes);
        }


        [HttpGet]// GET: PeriodiciteController/Create
        public ActionResult AddOrEdit(int id = 0)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Periodicite periode = new Periodicite();
            if (id == 0)
            {
                return View(new Periodicite());

            }
            if (id != 0)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Periodicite/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    periode = JsonConvert.DeserializeObject<Periodicite>(data);

                }

            }

            return View(periode);
        }

        // POST: PeriodiciteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit([Bind("Id, mois, annee")] Periodicite p)
        {
            if (ModelState.IsValid)
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                string data = JsonConvert.SerializeObject(p);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (p.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Periodicite", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Periodicite", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }


            return View(p);

        }

    }
}
