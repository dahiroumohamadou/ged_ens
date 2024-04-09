using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : ControllerBase
    {
        private IAssistance _demandeRepo;
        public DemandeController(IAssistance demandeRepo)
        {
            _demandeRepo = demandeRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ds = _demandeRepo.GetAll();
            if (ds == null)
                return NotFound();
            return Ok(ds);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var d = _demandeRepo.Get(id);
            if (d == null)
                return NotFound();
            return Ok(d);
        }

        [HttpPost]
        public IActionResult Add(Assistance d)
        {
            var res = _demandeRepo.Add(d);
            if (res <= 0)
                return NotFound();
            return Ok("Added " + res);
        }
        [HttpPut]
        public IActionResult Update(Assistance d)
        {
            var res = _demandeRepo.Update(d);
            if (res <= 0)
                return NotFound();
            return Ok("Updated " + res);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _demandeRepo.Delete(id);
            if (res <= 0)
                return NotFound();
            return Ok("Delete " + res);
        }
        [HttpGet]
        [Route("Membre/{id}")]
        public IActionResult GetByIdMembre(int id)
        {
            var assistances = _demandeRepo.GetByIdMembre(id);
            if (assistances == null)
                NotFound();
            return Ok(assistances);
        }
    }
}
