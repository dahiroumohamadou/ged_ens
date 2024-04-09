using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace soft.Controllers
{
    //[Authorize]
    public class AssistanceController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;

        public AssistanceController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Assistance> list = new List<Assistance>();
            try
            {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage res =_httpClient.GetAsync(_httpClient.BaseAddress+"/Demande").Result;
                if(res.IsSuccessStatusCode)
                {
                    string data=res.Content.ReadAsStringAsync().Result;
                    list=JsonConvert.DeserializeObject<List<Assistance>>(data);
                    ViewBag.DataSource=list;
                }

            }catch (Exception ex)
            {
                
            }
            return View(list);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0)
        {
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            OnloadMembre();
            //OnloadTypeAssistance();
            Assistance d=new Assistance();
            if (id == 0) {
                return View(new Assistance());
            }
            else
            {
                HttpResponseMessage res=_httpClient.GetAsync(_httpClient.BaseAddress+ "/Demande/" + id).Result;
                if(res.IsSuccessStatusCode)
                {
                    string data=res.Content.ReadAsStringAsync().Result;
                    d=JsonConvert.DeserializeObject<Assistance>(data);
                }
            }
            return View(d);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, Objet, Type, Proposition, Date, Montant, MembreId")]  Assistance d)
        {
            if(ModelState.IsValid)
            {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                OnloadMembre();
                //OnloadTypeAssistance();
                string data=JsonConvert.SerializeObject(d);
                StringContent content=new StringContent(data, Encoding.UTF8, "application/json");
                if (d.Id==0)
                {
                    d.Statut = 1;
                    HttpResponseMessage res=_httpClient.PostAsync(_httpClient.BaseAddress+ "/Demande", content).Result;
                    if(res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Demande", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");   
             
        }
        [HttpGet]
        public IActionResult Proposition(int id)
        {
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            OnloadMembre();
            //OnloadTypeAssistance();
            Assistance d = new Assistance();
            if (id == 0)
            {
                return View(new Assistance());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Demande/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    d = JsonConvert.DeserializeObject<Assistance>(data);
                }
            }
            return View(d);
        }
        [HttpPost, ActionName("Proposition")]
        [ValidateAntiForgeryToken]
        public IActionResult Proposition([Bind("Id, Objet, Type, Proposition, Date, Montant, MembreId")] Assistance a)
        {
            // statut pass à l'etat 1
            a.Statut = 1;
            if (ModelState.IsValid)
            {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                OnloadMembre();
                //OnloadTypeAssistance();
                string data = JsonConvert.SerializeObject(a);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
             
               HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Demande", content).Result;
               if (res.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index");
               }
                
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(ModelState.IsValid) {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                OnloadTypeAssistance();
               if (id == 0)
                {
                    BadRequest();
                }
                else
                {
                    HttpResponseMessage res=_httpClient.DeleteAsync(_httpClient.BaseAddress+ "/Demande/" + id).Result;
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
            List<Membre> list = new List<Membre>();
            if (ModelState.IsValid)
            {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "Your token");
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
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
        public void OnloadTypeAssistance()
        {
            List<string> types = new List<string>();
            if (ModelState.IsValid)
            {
                    types.Insert(0, "Choose Type");
                    types.Insert(1, "Deuil");
                    types.Insert(2, "Maladie");
                    types.Insert(3, "Naissance");
                    types.Insert(4, "Mariage");
                    types.Insert(5, "Deuil");
                ViewBag.Types = types;
            }
        }
    }
}
