using ged.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.FileUploadService;
using System;
using System.Net;
using System.Text;

namespace ged.Controllers
{
    public class AutreController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        private readonly IFileUploadService _uploadService;
        private readonly IWebHostEnvironment _environment;
        public string path;
        public string type = "AUTRE";
        public AutreController(IFileUploadService fileUploadService, IWebHostEnvironment environment)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            _uploadService = fileUploadService;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Doc> arretes = new List<Doc>();
            try
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/types/" + type).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    arretes = JsonConvert.DeserializeObject<List<Doc>>(data);
                    ViewBag.DataSource = arretes;
                }
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }
            return View(arretes);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc autre = new Doc();
            if (id == 0)
            {
                return View(new Doc());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    autre = JsonConvert.DeserializeObject<Doc>(data);
                }

            }
            return View(autre);

        }
        [HttpPost, ActionName("AddOrEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id, Numero, Source, Objet, DateSign, TypeDoc, Fichier")] Doc autre)
        {
            int existe = 0;
            Doc other= new Doc();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                autre.TypeDoc = "AUTRE";
                string data = JsonConvert.SerializeObject(autre);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (autre.Id == 0)
                {
                    HttpResponseMessage exist = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/others/" + autre.Source + "/" + autre.Numero + "/" + autre.DateSign ).Result;
                    if (exist.IsSuccessStatusCode)
                    {
                        existe = 1;
                        TempData["AlertMessage"] = "Document already exist.....";
                        return RedirectToAction("Index");
                    }
                    if (existe == 0)
                    {
                        HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            TempData["AlertMessage"] = "Document added successfully.....";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["AlertMessage"] = "Error saving .....";
                            return RedirectToAction("Index");
                        }
                    }


                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "Document Updated successfully.....";
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken] 
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Document deleted successfully.....";
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");

        }
       

        [HttpGet]
        public IActionResult AddPdf(int id)
        {

            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc autre = new Doc();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                autre = JsonConvert.DeserializeObject<Doc>(data);
            }
            return View(autre);
        }
        [HttpPost, ActionName("AddPdf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPdf([Bind("Id, Numero, Source, Objet, DateSign, TypeDoc, Fichier")] Doc a, IFormFile pdf)
        {

            if (pdf != null)
            {
                // update fichier
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                if (ModelState.IsValid)
                {
                    a.Fichier = 1;
                    string data = JsonConvert.SerializeObject(a);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        // copy fichier sur le serveur
                        path = await _uploadService.UploadPdfFileAutreAsync(pdf, a);
                        TempData["AlertMessage"] = "Document added successfully.....";
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { d.Id });
            }
            return View();
        }
        [HttpGet]
        public IActionResult showPdf(int id)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc aa = new Doc();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                aa = JsonConvert.DeserializeObject<Doc>(data);
                string? num = "Autre_" + aa.Numero + "_Du " + aa.DateSign;
                var replacement = num.Replace('/', '_');
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/doc/pdf/autres/", replacement + ".pdf");
                WebClient client = new WebClient();
                byte[] FileBuffer = client.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    return File(FileBuffer, "application/pdf");

                }
            }
            return View(aa);
        }
    }
}
