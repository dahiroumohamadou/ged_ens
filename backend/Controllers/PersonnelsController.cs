using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelsController : Controller
    {
        private IPersonnel _personnelRepo;
        public PersonnelsController(IPersonnel personnelrepo) {
            _personnelRepo = personnelrepo;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var resultat = _personnelRepo.GetAll();
            if (resultat == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(resultat);
            }

        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Personnel p) {
            int a =_personnelRepo.Add(p);
            if(a == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok("Added " + a);
            }
        }
        [HttpPut]
        [Route("Edit")]
        public IActionResult Update(Personnel p)
        {
            var a=_personnelRepo.Update(p);
            if( a == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok("Updated" + a);
            }
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var a = _personnelRepo.Delete(id);
            if (a <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok("Delete : " + a);
            }
        }
        [HttpGet]
        [Route("GetByMatricule")]
        public IActionResult GetByMatricule(string matriucle) { 
            var p=_personnelRepo.GetByMatricule(matriucle);
            if (p == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }           
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var p=_personnelRepo.GetById(id);
            if(p == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }
        }
      
      
    }
}
