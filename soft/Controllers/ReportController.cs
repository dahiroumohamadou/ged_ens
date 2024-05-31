using ged.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ged.Controllers
{
    public class ReportController : Controller
    {
        public ReportController()
        {

        }
        // GET: ReportController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult print(string annee)
        {
            try
            {
                DocumentReport docReport = new DocumentReport();
                byte[] bytes = docReport.prepareReportDocByYear(annee);
                return File(bytes, "application/pdf");
            }
            catch
            {
                TempData["AlertMessage"] = "ERROR.....";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost, ActionName("test")]
        [ValidateAntiForgeryToken]
        public IActionResult test()
        {
           
           
                TempData["AlertMessage"] = "bonjour admin";
            
            return RedirectToAction(nameof(Index));
        }
        // GET: ReportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
