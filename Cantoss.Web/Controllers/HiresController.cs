﻿using Microsoft.AspNetCore.Mvc;

namespace Cantoss.Web.Controllers
{
    public class HiresController : Controller
    {
        // GET: ResumeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResumeController/Details/5
        public ActionResult Loader(string hireid)
        {
            return View("loader",hireid);
        }

        // GET: ResumeController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Hiring()
        {
            return View();
        }

        // POST: ResumeController/Create
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

        // GET: ResumeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResumeController/Edit/5
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

        // GET: ResumeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResumeController/Delete/5
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
