using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cantoss.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        // GET: LearnController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LearnController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LearnController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LearnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LearnController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LearnController/Edit/5
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

        // GET: LearnController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LearnController/Delete/5
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
