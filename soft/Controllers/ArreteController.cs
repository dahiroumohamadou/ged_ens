using ged.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.FileUploadService;
using soft.Models;
using System.Net;
using System.Text;

namespace ged.Controllers
{
    public class ArreteController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        private readonly IFileUploadService _uploadService;
        private readonly IWebHostEnvironment _environment;
        public string path;
        public string type = "ARR";
        public ArreteController(IFileUploadService fileUploadService, IWebHostEnvironment environment)
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
            Doc ar = new Doc();
            OnloadCycle();
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
                    ar = JsonConvert.DeserializeObject<Doc>(data);
                }

            }
            return View(ar);

        }
        [HttpPost, ActionName("AddOrEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id, Source, Numero, DateSign, TypeDoc, AnneeAcademique, CycleId, Fichier")] Doc a)
        {
            int existe = 0;
            Doc arr= new Doc();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                OnloadCycle();
                a.TypeDoc = "ARR";
                string data = JsonConvert.SerializeObject(a);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (a.Id == 0)
                {
                    HttpResponseMessage exist = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/arretes/"+a.Source+"/"+a.Numero+"/"+a.DateSign+"/"+a.AnneeAcademique+"/"+a.CycleId).Result;
                    if (exist.IsSuccessStatusCode)
                    {
                        existe = 1;
                        TempData["AlertMessage"] = "Arrete already exist.....";
                        return RedirectToAction("Index");
                    }
                    if (existe == 0)
                    {
                        HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            TempData["AlertMessage"] = "Arrete added successfully.....";
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
                        TempData["AlertMessage"] = "Arrete Updated successfully.....";
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
                    TempData["AlertMessage"] = "Arrete deleted successfully.....";
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");

        }
        [NonAction]
        public void OnloadCycle()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Cycle> listCycle = new List<Cycle>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cycles").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    listCycle = JsonConvert.DeserializeObject<List<Cycle>>(data);
                    Cycle cycleDefault = new Cycle() { Id = 0, Code = "Choose a cycle" };
                    listCycle.Insert(0, cycleDefault);
                    ViewBag.Cycles = listCycle;
                }
            }
        }
        
        [HttpGet]
        public IActionResult AddPdf(int id)
        {

            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc ar = new Doc();
            OnloadCycle();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                ar = JsonConvert.DeserializeObject<Doc>(data);
            }
            return View(ar);
        }
        [HttpPost, ActionName("AddPdf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPdf([Bind("Id, Source, Numero, DateSign, TypeDoc, AnneeAcademique, CycleId, Fichier")] Doc a, IFormFile pdf)
        {

            if (pdf != null)
            {
                // update fichier
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                if (ModelState.IsValid)
                {
                    OnloadCycle();
                    a.Fichier = 1;
                    string data = JsonConvert.SerializeObject(a);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        // copy fichier sur le serveur
                        path = await _uploadService.UploadPdfFileArreteAsync(pdf, a);
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
            OnloadCycle();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                aa = JsonConvert.DeserializeObject<Doc>(data);
                string? num = "Arr_" + aa.Numero + "_Du " + aa.DateSign + "_Source" + aa.Source;
                var replacement = num.Replace('/', '_');
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/doc/pdf/arretes/", replacement + ".pdf");
                WebClient client = new WebClient();
                byte[] FileBuffer = client.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    return File(FileBuffer, "application/pdf");

                }
            }
            return View(aa);
        }
        //[HttpGet]
        //public IActionResult Fiche()
        //{
        //    return View();
        //}

        //[HttpPost, ActionName("printFiche")]
        //// [HttpGet, ActionName("printCarte")]
        //public IActionResult PrintFicheMembre(int id)
        //{
        //    MembreReport membreReport = new MembreReport();
        //    byte[] bytes = membreReport.prepareReportFiche(id);
        //    return File(bytes, "application/pdf");
        //}

        //[HttpPost, ActionName("printMembres")]
        //public IActionResult PrintMembres()
        //{
        //    MembreReport membreReport = new MembreReport();
        //    byte[] bytes = membreReport.prepareReportMembres();
        //    return File(bytes, "application/pdf");
        //}
    }
}
