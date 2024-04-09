using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace soft.Controllers
{
    // [Authorize]
    public class PaiementController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        public PaiementController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Paiement> list = new List<Paiement>();
            OnloadMembre();
            OnloadPeriodes();
            try
            {
                HttpResponseMessage res=_httpClient.GetAsync(_httpClient.BaseAddress+"/Paiement").Result;
                if(res.IsSuccessStatusCode) {
                    string data=res.Content.ReadAsStringAsync().Result;
                    list=JsonConvert.DeserializeObject<List<Paiement>>(data);   
                    ViewBag.DataSource = list;
                }
            }catch (Exception ex)
            {

            }
            return View(list);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0) {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Paiement p =new Paiement();
            OnloadMembre();
            OnloadPeriodes();
            if (id == 0)
            {
                return View(new Paiement());
            }
            else
            {
                HttpResponseMessage res=_httpClient.GetAsync(_httpClient.BaseAddress+"/Paiement/"+id).Result;
                if(res.IsSuccessStatusCode)
                {
                    string data= res.Content.ReadAsStringAsync().Result;
                    p=JsonConvert.DeserializeObject<Paiement>(data);
                }
            }
            return View(p);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, MembreId, PeriodiciteId, Montant, datePaiement")] Paiement p) {
            
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                string data=JsonConvert.SerializeObject(p);
                StringContent content= new StringContent(data, Encoding.UTF8, "application/json");
                if(p.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Paiement", content).Result;
                    if(res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Paiement", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    HttpResponseMessage res=_httpClient.DeleteAsync(_httpClient.BaseAddress+"/Paiement/"+id).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }  
            }
            return RedirectToAction("Index");
        }

        [NonAction]
        public void OnloadMembre()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Membre> list = new List<Membre>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Membre>>(data);
                    Membre defaultMembre = new Membre() { Id = 0, Noms = "Choose  Membre" };
                    list.Insert(0, defaultMembre);
                    ViewBag.Membres = list;
                }
            }
        }
        [NonAction]
        public void OnloadPeriodes()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Periodicite> list = new List<Periodicite>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Periodicite").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Periodicite>>(data);
                    Periodicite defaultPeriodicite = new Periodicite() { Id = 0, mois = "Choose a period" };
                    list.Insert(0, defaultPeriodicite);
                    ViewBag.Periodes = list;
                }
            }
        }
    }
}
