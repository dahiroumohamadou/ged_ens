using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CyclesController : ControllerBase
    {
        private ICycle _cycleRepo;
        public CyclesController(ICycle cycleRepo)
        {
            _cycleRepo = cycleRepo;
        }
        // GET: api/<CycleController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var cys = _cycleRepo.GetAll();
            if (cys != null)
                return Ok(cys);
            return NotFound();
        }

        // GET api/<CycleController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _cycleRepo.GetById(id);
            if (res==null)
                return BadRequest();
            return Ok(res);
        }

        // POST api/<CycleController>
        [HttpPost]
        public IActionResult Add(Cycle c)
        {
            var res = _cycleRepo.Add(c);
            if (res <= 0)
                return BadRequest();
            return Ok("Added :" + res);
        }

        // PUT api/<CycleController>/5
        [HttpPut]
        public IActionResult Updated(Cycle c)
        {
            var res = _cycleRepo.Update(c);
            if (res <= 0)
                return BadRequest();
            return Ok("Updated : " + res);
        }

        // DELETE api/<CycleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _cycleRepo.Delete(id);
            if (res <= 0)
                return BadRequest();
            return Ok("Deleted : " + res);
        }
    }
}
