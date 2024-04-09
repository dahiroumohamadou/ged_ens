using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembreController : ControllerBase
    {
        private IMembre _membreRepo;
        public MembreController(IMembre membreRepo)
        {
            _membreRepo = membreRepo;
        }
        [HttpGet]
        //public IActionResult GetAll() { 
        //    var membres= _membreRepo.GetAll();
        //    if (membres == null)
        //        return NotFound();
        //    return Ok(membres);           
        //}
        [HttpGet]
        public async Task<IEnumerable<Membre>> GetAll()
        {
            var membres = await _membreRepo.GetAll();
            return membres;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var m= _membreRepo.Get(id);
            if (m == null)
                return NotFound();
            return Ok(m);
        }
        [HttpPost]
        public IActionResult Add(Membre membre)
        {
            int res = _membreRepo.Add(membre);
            if(res <= 0)
                return NotFound();
            return Ok("Added "+ res);
        }
        [HttpPut]
        public IActionResult Update(Membre membre)
        {
            int res = _membreRepo.Update(membre);
            if(res <= 0)
                return NotFound();
            return Ok("Updated" + res);

        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            int res= _membreRepo.Delete(id);
            if(res <= 0)
                return NotFound();
            return Ok("Delete " + res);
        }
        [HttpGet]
        [Route("/Fiche/{id}")]
        public IActionResult printFiche(int id)
        {
            var document = new PdfDocument();
            string htmlContent = "<h1> Hello world ! </h1>";
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms=new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string filename = "Fiche_" + id + ".pdf";
            return File(response,"application/pdf",filename);
        }


    }
}
