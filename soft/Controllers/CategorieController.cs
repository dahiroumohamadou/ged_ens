using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace soft.Controllers
{
    //[Authorize]
    public class CategorieController : Controller
    {
        Uri baseAdress= new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        
        public CategorieController()
        {
            _httpClient = new HttpClient();
           _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Categorie> list = new List<Categorie>();
            try
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Categorie").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Categorie>>(res);
                    ViewBag.DataSource = list;
                }
            }
            catch (Exception ex)
            {

            }
           
            return View(list);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0) { 
            Categorie categorie = new Categorie();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (id == 0)
            {
                return View(new Categorie());
            }
            else
            {
                HttpResponseMessage res=_httpClient.GetAsync(_httpClient.BaseAddress + "/Categorie/"+id).Result;
                if(res.IsSuccessStatusCode)
                {
                    string data=res.Content.ReadAsStringAsync().Result;
                    categorie=JsonConvert.DeserializeObject<Categorie>(data);
                }
            }
            return View(categorie);
                

        }
        [HttpPost]   
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, libele, MontantAdhesion, MontantCotisation")] Categorie c) {
            if(ModelState.IsValid)
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                string data =JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if(c.Id == 0)
                {
                    HttpResponseMessage res=_httpClient.PostAsync(_httpClient.BaseAddress +"/Categorie", content).Result;
                    if(res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Categorie", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(c);
        }
        public IActionResult Delete() { 
            return View();
        }
    }
}
