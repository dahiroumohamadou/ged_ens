using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private ICategorie _categorieRepo;
        public CategorieController(ICategorie Icategorie)
        {
            _categorieRepo = Icategorie;
        }
        [HttpGet]
        //[Authorize]
        public IActionResult GetAll()
        {
            var categories = _categorieRepo.GetAll();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id) { 
            var c=_categorieRepo.Get(id);
            if(c == null)
                return NotFound();
            return Ok(c);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult Add(Categorie c) {
            int res=_categorieRepo.Add(c);
            if(res <= 0)
                return NotFound();
            return Ok("Added"+res);
        }
        [HttpPut]
        public IActionResult Update(Categorie c)
        {
            int res=_categorieRepo.Update(c);
            if (res <= 0)
                return NotFound();
            return Ok("Updated" + res);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            var res= _categorieRepo.Delete(id);
            if (res <= 0)
                return NotFound();
            return Ok("Delete" + res);
        }

    }
}
