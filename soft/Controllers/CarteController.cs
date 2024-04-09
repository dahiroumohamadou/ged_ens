using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.FileUploadService;
using soft.Models;
using soft.Reports;

namespace soft.Controllers
{
    public class CarteController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        private readonly IFileUploadService _uploadService;
        private readonly IWebHostEnvironment _environment;
        public string pathPhoto;
        public CarteController(IFileUploadService fileUploadService, IWebHostEnvironment en)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            _uploadService = fileUploadService;
            _environment = en;

        }
        public IActionResult Index()
        {
            List<Membre> list = new List<Membre>();
            try
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Membre>>(data);
                    ViewBag.DataSource = list;
                }
            }
            catch (Exception ex)
            {

            }
            return View(list);
        }
        [HttpGet]
        public IActionResult AddProfil(int id) {

            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Membre membre = new Membre();
            OnloadCategories();
            OnloadStructures();
           HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    membre = JsonConvert.DeserializeObject<Membre>(data);
                
                }

            
            return View(membre);
        }
        [HttpPost, ActionName("AddProfil")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddProfil([Bind("Id, Noms, Prenoms, Matricule, Fonction, StructureAffectation, PhoneNumber, StructureId, CategorieId, MontantAdhesion, DateAdhesion")] Membre membre, IFormFile ph)
        {
            if(ph!= null) {
                pathPhoto= await _uploadService.UploadFileAsync(ph, membre);
                return RedirectToAction("Details", new { membre.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id) {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Membre membre = new Membre();
            OnloadCategories();
            OnloadStructures();

            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
              
                string data = res.Content.ReadAsStringAsync().Result;
                membre = JsonConvert.DeserializeObject<Membre>(data);
                var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/img/photos", membre.Matricule + ".png");
                ViewBag.Photo= filePath;
            }


            return View(membre);
        }
        [HttpPost, ActionName("printCarte")]
        // [HttpGet, ActionName("printCarte")]
        public IActionResult PrintCarteMembre(int id)
        {
            MembreReport membreReport = new MembreReport();
            byte[] bytes = membreReport.prepareReportCarte(id);
            return File(bytes, "application/pdf");
        }
        [NonAction]
        public void OnloadCategories()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Categorie> list = new List<Categorie>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Categorie").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Categorie>>(data);
                    Categorie defaultCategorie = new Categorie() { Id = 0, libele = "Choose a category" };
                    list.Insert(0, defaultCategorie);
                    ViewBag.Categories = list;
                }
            }
        }
        [NonAction]
        public void OnloadStructures()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Structure> listStr = new List<Structure>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Structure").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    listStr = JsonConvert.DeserializeObject<List<Structure>>(data);
                    Structure strDefault = new Structure() { Id = 0, Code = "Choose a structure" };
                    listStr.Insert(0, strDefault);
                    ViewBag.Structures = listStr;
                }
            }
        }
    }
}
