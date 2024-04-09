using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodiciteController : ControllerBase
    {
        private IPeriodicite _periodiciteRepo;
        public PeriodiciteController(IPeriodicite periodicitereop)
        {
            _periodiciteRepo = periodicitereop;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var res = _periodiciteRepo.GetAll();
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var res=_periodiciteRepo.GetById(id);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }

        }
        [HttpPost]
        public IActionResult Add(Periodicite periodicite)
        {
            var res=_periodiciteRepo.Add(periodicite);
            if (res>0)
            {
                return Ok("Added : " + res);
            }
            else
            {
                return BadRequest();
            }
            
        }
        [HttpPut]
        public IActionResult Edit(Periodicite periodicite)
        {
            var res=_periodiciteRepo.Update(periodicite);
            if (res <= 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok("Updated " + res);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var res = _periodiciteRepo.Delete(id);
            if (res <= 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok("Delete : " + res);
            }

        }


    }
}
