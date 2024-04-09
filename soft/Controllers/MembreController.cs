using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using soft.Models;
using Syncfusion.EJ2.Schedule;
using System.Net.Http;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using soft.Reports;
using soft.FileUploadService;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace soft.Controllers
{
    [Authorize]
    public class MembreController : Controller
    {
       Uri baseAdress= new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        private readonly IFileUploadService _uploadService;
        public string pathPhoto;
        

        public MembreController(IFileUploadService fileUploadService) { 
           
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdress;
            _uploadService = fileUploadService;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        }
        [HttpGet]
        public string getToken(int id=1)
        {
            return "";
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Membre> list = new List<Membre>();
            try
            {
                //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage res =_httpClient.GetAsync(_httpClient.BaseAddress+"/Membre").Result;
                if(res.IsSuccessStatusCode)
                {
                    string data=res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Membre>>(data);
                    ViewBag.DataSource=list;
                }
            }catch (Exception ex)
            {

            }
            return View(list);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id=0)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Membre membre = new Membre();
            OnloadCategories();
            OnloadStructures();
            if (id == 0)
            {
                return View(new Membre());
            }
            else
            {
                HttpResponseMessage res=_httpClient.GetAsync(_httpClient.BaseAddress+"/Membre/"+id).Result;
                if( res.IsSuccessStatusCode )
                {
                    string data= res.Content.ReadAsStringAsync().Result;
                    membre=JsonConvert.DeserializeObject<Membre>(data);
                    TempData["photo"] = membre.Matricule;
                }
              
            }
            return View(membre);
            
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id, Noms, Prenoms, Matricule, Fonction, StructureAffectation, PhoneNumber, StructureId, CategorieId, MontantAdhesion, DateAdhesion")]  Membre membre)
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            if (ModelState.IsValid)
            {
                OnloadCategories();
                OnloadStructures();
                string data=JsonConvert.SerializeObject(membre);
                StringContent content= new StringContent(data, Encoding.UTF8, "application/json");
                if(membre.Id==0)
                {
                    
                    HttpResponseMessage res=_httpClient.PostAsync(_httpClient.BaseAddress+"/Membre", content).Result;
                    if(res.IsSuccessStatusCode )
                    {
                        //if(photo!=null)
                        //    pathPhoto= await _uploadService.UploadFileAsync(photo, membre.Matricule);

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage res = _httpClient.PutAsync(_httpClient.BaseAddress + "/Membre", content).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        //if (photo != null)
                        //    pathPhoto = await _uploadService.UploadFileAsync(photo, membre.Matricule);
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Traitement(int id)
        {

            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            Membre membre = new Membre();
            OnloadCategories();
            OnloadStructures();
            if (id == 0)
            {
                return View(new Membre());
            }
            else
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    membre = JsonConvert.DeserializeObject<Membre>(data);
                }

            }
            return View(membre);
        }
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken] 
        public IActionResult Delete(int id)
        {

            if (id > 0)
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");

        }
        

        [NonAction]
        public void OnloadCategories()
        {
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            List<Categorie> list=new List<Categorie>();
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Categorie").Result;
                if (res.IsSuccessStatusCode)
                {
                    string data=res.Content.ReadAsStringAsync().Result; 
                    list=JsonConvert.DeserializeObject<List<Categorie>>(data);
                    Categorie defaultCategorie = new Categorie(){ Id = 0, libele="Choose a category" };
                    list.Insert(0, defaultCategorie);
                    ViewBag.Categories=list;
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
                    Structure strDefault=new Structure() { Id=0, Code="Choose a structure"};
                    listStr.Insert(0, strDefault);
                    ViewBag.Structures = listStr;
                }
            }
        }

        [HttpPost, ActionName("printFicheM")]
        public void printFiche(int id)
        {
            Membre membre=new Membre();
            HttpResponseMessage res = _httpClient.GetAsync(_httpClient.BaseAddress + "/Membre/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                membre = JsonConvert.DeserializeObject<Membre>(data);
            }
            Document pdoc=new Document(PageSize.A4, 20f, 20f, 30f, 30f);
            PdfWriter pWriter=PdfWriter.GetInstance(pdoc, new FileStream("C:\\Users\\Public\\Documents\\Fiche.pdf", FileMode.Create));
            pdoc.Open();
            Paragraph p = new Paragraph("Hello wold : "+membre.Noms+DateTime.Today.Year);
            pdoc.Add(p);
            pdoc.Close();


        }
        [HttpGet]
        public IActionResult Fiche()
        {
            return View();
        }

        [HttpPost, ActionName("printFiche")] 
       // [HttpGet, ActionName("printCarte")]
        public IActionResult PrintFicheMembre(int id)
        {
            MembreReport membreReport=new MembreReport();
            byte[] bytes=membreReport.prepareReportFiche(id);
            return File(bytes, "application/pdf");
        }
        
        [HttpPost, ActionName("printMembres")] 
        public IActionResult PrintMembres()
        {
            MembreReport membreReport = new MembreReport();
            byte[] bytes = membreReport.prepareReportMembres();
            return File(bytes, "application/pdf");
        }
        
    }
}
