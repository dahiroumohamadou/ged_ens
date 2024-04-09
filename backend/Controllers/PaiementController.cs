using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private IPaiement _paiementRepo;
        public PaiementController(IPaiement paiementRepo)
        {
            _paiementRepo = paiementRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var paiements= _paiementRepo.GetAll();
            if(paiements==null)
                return NotFound();
            return Ok(paiements);

        }
        [HttpGet]
        [Route("Membre/{id}")]
        public IActionResult GetByIdMembre(int id)
        {
            var paiements = _paiementRepo.GetByIdMembre(id);
            if (paiements == null)
                return NotFound();
            return Ok(paiements);

        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id) {
            var paiement=_paiementRepo.GetById(id);
            if(paiement==null)
                return NotFound();
            return Ok(paiement);
        }
        [HttpPost]
        public IActionResult Add(Paiement p)
        {
            int res=_paiementRepo.Add(p);
            if (res <= 0)
                return NotFound();
            return Ok("Added" +res);
        }
        [HttpPut]
        public IActionResult Update(Paiement p)
        {
            int res=_paiementRepo.Update(p);
            if(res <= 0)
                return NotFound();
            return Ok("Updated" + res);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            int res = _paiementRepo.Delete(id);
            if (res <= 0)
                return NotFound();
            return Ok("Delete" + res);

        }
       
    }
}
