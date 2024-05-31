using ged.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.FileUploadService;
using soft.Models;
using System.Net;
using System.Text;

namespace ged.Controllers
{
    [Authorize]
    //[Authorize (Roles ="admin")]
    public class PvController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api/v1");
        private readonly HttpClient _httpClient;
        private readonly IFileUploadService _uploadService;
        private readonly IWebHostEnvironment _environment;
        private int Id;
        public string path;
        public string type = "PV";
        public PvController(IFileUploadService fileUploadService, IWebHostEnvironment environment)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            _uploadService = fileUploadService;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Doc> pvs = new List<Doc>();
            try
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/types/"+type).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    pvs = JsonConvert.DeserializeObject<List<Doc>>(data);
                    ViewBag.DataSource = pvs;
                }
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }
            return View(pvs);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc pv = new Doc();
            OnloadCycle();
            OnloadFiliere();
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
                    pv = JsonConvert.DeserializeObject<Doc>(data);
                }

            }
            return View(pv);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult test([Bind("Id, Promotion, AnneeSortie, Source, TypeDoc, Session, CycleId, FiliereId")] Doc p)
        {
            TempData["AlertMessage"] = "Id="+p.Id +" Promo= "+p.Promotion +" Source= "+p.Source +" type doc = "+p.TypeDoc +" session = "+p.Session +" cycle ="+p.CycleId +" filiere = "+p.FiliereId;
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("AddOrEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id, Promotion, AnneeSortie, Source, TypeDoc, Session, CycleId, FiliereId")] Doc p)
        {
            Doc pv = new Doc();
            int existe = 0;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                OnloadCycle();
                OnloadFiliere();
                p.TypeDoc = "PV";

                string data = JsonConvert.SerializeObject(p);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (p.Id == 0)
                {
                    HttpResponseMessage exist = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/pvs/" + p.Source + "/" + p.Session + "/" + p.Promotion + "/" + p.AnneeSortie + "/" + p.CycleId+"/"+p.FiliereId).Result;
                    if (exist.IsSuccessStatusCode)
                    {
                        existe = 1;
                        TempData["AlertMessage"] = "Pv already exist.....";
                        return RedirectToAction("Index");
                    }
                    if (existe == 0)
                    {
                        HttpResponseMessage res = _httpClient.PostAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            TempData["AlertMessage"] = "Pv added successfully.....";
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
                        TempData["AlertMessage"] = "Pv Updated successfully.....";
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
                    TempData["AlertMessage"] = "Pv deleted successfully.....";
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
        [NonAction]
        public void OnloadFiliere()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Filiere> filieres = new List<Filiere>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Filieres").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    filieres = JsonConvert.DeserializeObject<List<Filiere>>(data);
                    Filiere filiereDefault = new Filiere() { Id = 0, Libele = "Choose filiere" };
                    filieres.Insert(0, filiereDefault);
                    ViewBag.Filieres = filieres;
                }
            }
        }
        [HttpGet]
        public IActionResult AddPdf(int id)
        {

            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc p = new Doc();
            OnloadCycle();
            OnloadFiliere();
          
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                p = JsonConvert.DeserializeObject<Doc>(data);
            }
            return View(p);
        }
        [HttpPost, ActionName("AddPdf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPdf([Bind("Id, Source, TypeDoc, Session, Fichier, Promotion, AnneeSortie, CycleId, FiliereId")] Doc p, IFormFile pdf)
        {
            
            if (pdf != null)
            {
                // update fichier
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                if (ModelState.IsValid)
                {
                    OnloadCycle();
                    OnloadFiliere() ;
                    p.Fichier = 1;
                    string data = JsonConvert.SerializeObject(p);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Documents", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        // copy fichier sur le serveur
                        path = await _uploadService.UploadPdfFilePvsAsync(pdf, p);
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
            Doc p = new Doc();
            OnloadCycle();
            OnloadFiliere();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                p = JsonConvert.DeserializeObject<Doc>(data);
                string? num ="Pv_"+ p.Promotion + "_A" + p.AnneeSortie + "_S" + p.Session + "_C" + p.CycleId + "_F" + p.FiliereId + "_Source_" + p.Source;
                var replacement = num.Replace('/', '_');
                replacement = replacement.Replace(' ', '_');
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/doc/pdf/pvs/", replacement + ".pdf");
                WebClient client = new WebClient();
                byte[] FileBuffer = client.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    return File(FileBuffer, "application/pdf");

                }
            }
            return View(p);
        }
        public Boolean existeFichier(int id)
        {
            Boolean resultat = false;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Doc p = new Doc();
            OnloadCycle();
            OnloadFiliere();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Documents/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                p = JsonConvert.DeserializeObject<Doc>(data);
                string? num = p.Promotion + "_A" + p.AnneeSortie + "_S" + p.Session + "_C" + p.Cycle.Code + "_F" + p.Filiere.Libele;
                var replacement = num.Replace('/', '_');
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/doc/pdf/pvs", replacement + ".pdf");
                WebClient client = new WebClient();
                byte[] FileBuffer = client.DownloadData(filePath);
                if (FileBuffer != null)
                {
                    resultat = true;

                }
            }
            return resultat;
        }
        public void Alert(string message, string notificationType)
        {
            var msg = "swal('" + notificationType.ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            TempData["notification"] = msg;
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

