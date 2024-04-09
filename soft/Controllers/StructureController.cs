using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace soft.Controllers
{
    public class StructureController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;

        public StructureController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Structure> list = new List<Structure>();
            try
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Structure").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Structure>>(res);
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
            Structure structure = new Structure();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (id == 0)
            {
                return View(new Structure());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Structure/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    structure = JsonConvert.DeserializeObject<Structure>(data);
                }
            }
            return View(structure);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, Code, Libele")] Structure s)
        {
            if (ModelState.IsValid)
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                string data = JsonConvert.SerializeObject(s);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (s.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Structure", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Structure added successfully.....";
                        return RedirectToAction("Index");
                        
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Structure", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Structure updated successfully.....";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(s);
        }
    }
}
