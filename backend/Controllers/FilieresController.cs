using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilieresController : ControllerBase
    {
        private IFiliere _filereRepo;
        public FilieresController(IFiliere filiereRepo)
        {
            _filereRepo = filiereRepo;
        }
        // GET: api/<FiliereController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var fs = _filereRepo.GetAll();
            if (fs != null)
                return Ok(fs);
            return NotFound();
        }

        // GET api/<FiliereController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _filereRepo.GetById(id);
            if (res ==null)
                return BadRequest();
            return Ok(res);
        }

        // POST api/<FiliereController>
        [HttpPost]
        public IActionResult Add(Filiere f)
        {
            var res = _filereRepo.Add(f);
            if (res <= 0)
                return BadRequest();
            return Ok("Added : " + res);
        }

        // PUT api/<FiliereController>/5
        [HttpPut]
        public IActionResult Update(Filiere f)
        {
            var res = _filereRepo.Update(f);
            if (res <= 0)
                return BadRequest();
            return Ok("Updated : " + res);
        }

        // DELETE api/<FiliereController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _filereRepo.Delete(id);
            if (res <= 0)
                return BadRequest();
            return Ok("Delete : " + res);
        }
    }
}
