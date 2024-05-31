using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using System.Text;

namespace ged.Controllers
{
    public class DiplomeController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        public DiplomeController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Cycle> list = new List<Cycle>();
            try
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cycles").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Cycle>>(res);
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
            Cycle cycle = new Cycle();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (id == 0)
            {
                return View(new Cycle());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cycles/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    cycle = JsonConvert.DeserializeObject<Cycle>(data);
                }
            }
            return View(cycle);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, Code, Libele")] Cycle c)
        {
            if (ModelState.IsValid)
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                string data = JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (c.Id == 0)
                {
                    HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Cycles", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Cycle added successfully.....";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Error saving or cycle exist .....";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Cycles", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Cycle updated successfully.....";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(c);
        }
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            if (id > 0)
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Cycle/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Cycle Deleted successfully.....";
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");

        }
    }
}
