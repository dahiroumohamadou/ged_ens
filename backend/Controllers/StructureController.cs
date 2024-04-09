using backend.Model;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureController : ControllerBase
    {
        private IStructure _strutureRepo;
        public StructureController(IStructure structueRepo)
        {
            _strutureRepo = structueRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var strs= _strutureRepo.GetAll();
            if(strs.Count == 0)
                return NotFound();
            return Ok(strs);
        }
        [HttpPost]
        public IActionResult Add(Structure structure)
        {
            int res=_strutureRepo.Add(structure);
            if(res <= 0)
                NotFound();
            return Ok("Added"+ res);
            
        }
        [HttpPut]
        public IActionResult Update(Structure structure)
        {
            int res=_strutureRepo.Update(structure);
            if(res <= 0)
                NotFound();
            return Ok("Updated"+ res);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            int res=_strutureRepo.Delete(id);
            if(res<= 0) 
                NotFound();
            return Ok("Deleted " + res);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var s=_strutureRepo.GetById(id);
            if(s==null)
                return NotFound();
            else
                return Ok(s);
        }
    }
}
