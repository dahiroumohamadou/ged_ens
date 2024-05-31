using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace ged.Controllers
{
    public class FiliereController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        public FiliereController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Filiere> list = new List<Filiere>();
            try
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Filieres").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Filiere>>(res);
                    ViewBag.DataSource = list;
                }
            }
            catch (Exception ex)
            {

            }

            return View(list);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            Filiere f = new Filiere();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (id == 0)
            {
                return View(new Filiere());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Filieres/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    f = JsonConvert.DeserializeObject<Filiere>(data);
                }
            }
            return View(f);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, Code, Libele")] Filiere f)
        {
            if (ModelState.IsValid)
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                string data = JsonConvert.SerializeObject(f);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (f.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Filieres", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Filiere added successfully.....";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["AlertMessage"] = "Error saving or Filiere exist .....";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Filieres", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Filiere updated successfully.....";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(f);
        }
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken] 
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Filieres/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Filiere deleted successfully.....";
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");

        }
    }
}
