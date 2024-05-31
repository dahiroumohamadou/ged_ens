using ged.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.FileUploadService;
using soft.Models;
using System.Net;
using System.Reflection.Metadata;
using System.Text;

namespace ged.Controllers
{
    public class CommuniqueController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        private readonly IFileUploadService _uploadService;
        private readonly IWebHostEnvironment _environment;
        public string path;
        public string type = "CRP";
        public CommuniqueController(IFileUploadService fileUploadService, IWebHostEnvironment environment)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            _uploadService = fileUploadService;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Doc> cs = new List<Doc>();
            try
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/types/" + "CRP").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    cs = JsonConvert.DeserializeObject<List<Doc>>(data);
                    ViewBag.DataSource = cs;
                }
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }
            return View(cs);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc c = new Doc();
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
                    c = JsonConvert.DeserializeObject<Doc>(data);
                }

            }
            return View(c);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id, Numero, Source, DateSign, Session, AnneeAcademique, TypeDoc, CycleId, Fichier")] Doc c)
        {
            int existe = 0;
            Doc cr = new Doc();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                OnloadCycle();
                c.TypeDoc = "CRP";
                string data = JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (c.Id == 0)
                {
                    HttpResponseMessage exist = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/communiques/" + c.Source + "/" + c.Numero + "/" + c.DateSign + "/" + c.AnneeAcademique + "/" + c.CycleId).Result;
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
                        TempData["AlertMessage"] = "Press release Updated successfully.....";
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
                    TempData["AlertMessage"] = "Press release deleted successfully.....";
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
            Doc c = new Doc();
            OnloadCycle();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<Doc>(data);
            }
            return View(c);
        }
        [HttpPost, ActionName("AddPdf")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddPdf([Bind("Id, Numero, Source, DateSign, Session, AnneeAcademique, TypeDoc, CycleId, Fichier")] Doc c, IFormFile pdf)
        {
            if (pdf != null)
            {
                

                // update fichier
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                if (ModelState.IsValid)
                {
                    OnloadCycle();
                    c.Fichier = 1;
                    string data = JsonConvert.SerializeObject(c);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        // copy fichier sur le serveur
                        path = await _uploadService.UploadPdfFileCommuniqueAsync(pdf, c);
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
            Doc c = new Doc();
            OnloadCycle();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<Doc>(data);
                string? num = "CRP_" + c.Numero + "_Du " + c.DateSign+"_Ses"+c.Session+"_AnneeAcad"+c.AnneeAcademique+"_cycl"+c.Cycle+"_source"+c.Source;
                var replacement = num.Replace('/', '_');
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/doc/pdf/communiques/", replacement + ".pdf");
                WebClient client = new WebClient();
                byte[] FileBuffer = client.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    return File(FileBuffer, "application/pdf");

                }
            }
            return View(c);
        }
        public Boolean existeFichier(int id)
        {
            Boolean resultat = false;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc c = new Doc();
            OnloadCycle();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                c = JsonConvert.DeserializeObject<Doc>(data);
                string? num = "CRP_"+c.Numero + " Du " + c.DateSign;
                var replacement = num.Replace('/', '_');
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/doc/pdf/arretes", replacement + ".pdf");
                WebClient client = new WebClient();
                byte[] FileBuffer = client.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    resultat = true;

                }
            }
            return resultat;
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
