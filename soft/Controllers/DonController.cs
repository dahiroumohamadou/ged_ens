using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace soft.Controllers
{
    public class DonController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        public DonController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Don> list = new List<Don>();
            try {
                HttpResponseMessage response=_httpClient.GetAsync(_httpClient.BaseAddress +"/v1/Don").Result;
                if(response.IsSuccessStatusCode)
                {
                    string res=response.Content.ReadAsStringAsync().Result;
                    list=JsonConvert.DeserializeObject<List<Don>>(res);
                    ViewBag.DataSource = list;
                }
            }catch (Exception ex)
            {
                ex.GetBaseException();
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0) {
            OnloadMembre();
            Don don = new Don();
            if(id == 0)
            {
                return View(new Don());
            }
            else
            {
                HttpResponseMessage response=_httpClient.GetAsync(_httpClient.BaseAddress+"/v1/Don/"+id).Result;
                if(response.IsSuccessStatusCode) {
                    string res= response.Content.ReadAsStringAsync().Result;
                    don= JsonConvert.DeserializeObject<Don>(res);
                }
            }
            return View(don);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, Type, Montant, Description, MembreId")] Don don) {
            if(ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(don);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (don.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/v1/Don", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/v1/Don", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult  Delete(int id)
        {
            if(id>0)
            {
                HttpResponseMessage res=_httpClient.DeleteAsync(_httpClient.BaseAddress+"/v1/Don/"+id).Result;
                if(res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
    }
}
