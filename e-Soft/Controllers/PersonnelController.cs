using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using e_soft.Models;
using System.Net.Http;
using System.Text;

namespace soft.Controllers
{
    public class PersonnelController : Controller
    {
        Uri baseAdresse = new Uri("http://localhost:5249/api");
        private readonly HttpClient _httpClient;
        public PersonnelController() {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAdresse;  
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<PersonnelViewModel> listPersonnel = new List<PersonnelViewModel>();
            try
            {
                HttpResponseMessage response=_httpClient.GetAsync(_httpClient.BaseAddress+"/Personnels/GetAll").Result;
                if(response.IsSuccessStatusCode)
                {
                    string data=response.Content.ReadAsStringAsync().Result;
                    listPersonnel=JsonConvert.DeserializeObject<List<PersonnelViewModel>>(data);
                    ViewBag.DataSource = listPersonnel;
                    return View(listPersonnel);
                }

            }catch (Exception ex)
            {
              Console.WriteLine(ex.Message);

            }
            return View(listPersonnel);
           
        }
       
        // Get : Personnel/AddOrEdit
        [HttpGet]
        public IActionResult AddOrEdit(int id=0)
        {
            PersonnelViewModel personnel = new PersonnelViewModel();
         
                if (id == 0)
                {
                    Console.WriteLine("resulat =" + personnel.Noms);
                    return View(new PersonnelViewModel());
                }
                if (id != 0)
                {
                    HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Personnels/GetById/" + id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        personnel = JsonConvert.DeserializeObject<PersonnelViewModel>(data);
                       
                        return View(personnel);
                    }
                   
                }
            return View(personnel);

        }
        // Post : Personnel/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id, Noms, Matricule, Grade, Sexe, LieuAffectation")] PersonnelViewModel p) {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(p);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                if (p.Id == 0)
                {
                    HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Personnels/Add", content).Result;
                    if (response.IsSuccessStatusCode )
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Personnels/Edit", content).Result;
                    if (response.IsSuccessStatusCode )
                    {
                        return RedirectToAction("Index");
                    }

                }           
            }
            return View(p);
        }
        // Post : Delete personnel
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Personnels/Delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                   Console.WriteLine("ok "+ response.Content);
                    ////return RedirectToAction(nameof(AddOrEdit));

                }
                else
                {
                    Console.WriteLine("non "+response.Content);
                    //return RedirectToAction(nameof(Index));

                }
               

            }
            return RedirectToAction(nameof(Index));


        }
    }
   
}
