using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private IDocument _docRepo;
        public DocumentsController(IDocument docRepo)
        {
            _docRepo = docRepo;
        }
        // GET: api/<DocumentController>
        [HttpGet]
        public IActionResult GetAll()
        {
           var ds= _docRepo.GetAll();
            if (ds != null)
                return Ok(ds);
            return NotFound();
        }

        // GET api/<DocumentController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var d= _docRepo.GetById(id);
            if (d != null)
                return Ok(d);
            return NotFound();
        }
        // GET api/<DocumentController/types/5
        [HttpGet("types/{type}")]
        public IActionResult GetByType(string type)
        {
            var d = _docRepo.GetAllByType(type);
            if (d != null)
                return Ok(d);
            return NotFound();
        }
        // GET api/<DocumentController>/years/2005
        [HttpGet("years/{annee}")]
        public IActionResult GetByAnnee(string annee)
        {
            var d = _docRepo.GetAllByAnnee(annee);
            if (d != null)
                return Ok(d);
            return NotFound();
        } 
        // POST api/<DocumentController>
        [HttpPost]
        public IActionResult Add(Doc d)
        {
            var res = _docRepo.Add(d);
            if (res > 0)
                return Ok("Added"+ res);
            return BadRequest();
        }
        [HttpGet("pvs/{source}/{session}/{promotion}/{anneeSortie}/{cycleId}/{filiereId}")]
        public IActionResult ExistePv(string source, string session, string promotion, string anneeSortie, int cycleId, int filiereId)
        {
            var pv = _docRepo.ExistePv(source, session, promotion, anneeSortie, cycleId, filiereId);
            if (pv!=null)
                return Ok(pv);
            return BadRequest();
        }
        [HttpGet("arretes/{source}/{numero}/{dateSign}/{anneeAca}/{cycleId}")]
        public IActionResult ExisteArr(string source, string numero, string dateSign, string anneeAca, int cycleId)
        {
            var arr = _docRepo.ExisteAr(source, numero, dateSign, anneeAca, cycleId);
            if (arr != null)
                return Ok(arr);
            return BadRequest();
        }
        [HttpGet("communiques/{source}/{numero}/{dateSign}/{session}/{anneeAca}/{cycleId}")]
        public IActionResult ExisteCrp(string source, string numero, string dateSign, string session, string anneeAca, int cycleId)
        {
            var crp = _docRepo.ExisteCrp(source, numero, dateSign, session, anneeAca, cycleId);
            if (crp != null)
                return Ok(crp);
            return BadRequest();
        }
        [HttpGet("others/{source}/{numero}/{dateSign}")]
        public IActionResult ExisteOther(string source, string numero, string dateSign)
        {
            var o = _docRepo.ExisteOthers(source, numero, dateSign);
            if (o != null)
                return Ok("exist: "+ o.Id);
            return NotFound();
        }
        // PUT api/<DocumentController>/5
        [HttpPut]
        public IActionResult Update(Doc d)
        {
            var res=_docRepo.Update(d);
            if (res > 0)
                return Ok("Updated :" + res);
            return BadRequest();
        }

        // DELETE api/<DocumentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res= _docRepo.Delete(id);
            if (res > 0)
                return Ok("Deleted " + res);
            return BadRequest();
        }
        //// Existe api/<DocumentController>/5
        //[HttpGet("exist/{type}")]
        //public IActionResult Existe(Doc d, string type)
        //{
        //    var res = _docRepo.Existe(d, type);
        //    if (res > 0)
        //        return Ok("Existe " + res);
        //    return BadRequest();
        //}
    }
}
