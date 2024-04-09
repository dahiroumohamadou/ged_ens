using backend.Model;
using backend.Repository.Implementations;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DonController : ControllerBase
    {
        private IDon _donRepo;
        public DonController(IDon donRepo)
        {
            _donRepo = donRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var dons= _donRepo.GetAll();
            if(dons == null)
                return NotFound();
            return Ok(dons);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var d=_donRepo.GetById(id);
            if(d == null)
                return NotFound();
            return Ok(d);
        }
        [HttpPost]
        public IActionResult Add(Don d)
        {
            int don= _donRepo.Add(d);
            if(don<=0)
                return NotFound();
            return Ok("Added"+don);
        }
        [HttpPut] 
        public IActionResult Update(Don don)
        {
           int res=_donRepo.Update(don);
            if(res<=0)
                return NotFound();
            return Ok("Updated "+res);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            int res=_donRepo.Delete(id);
            if(res<=0)
                return NotFound();
            return Ok(res);
        }
    }
}
