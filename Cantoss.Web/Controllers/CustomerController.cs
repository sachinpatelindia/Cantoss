﻿using Cantoss.Azure.Library.Cosmos;
using Cantoss.Library.Domain.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Cantoss.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICosmosDbHandler<Customer> _cosmosDbHandler;
        public CustomerController(ICosmosDbHandler<Customer> cosmosDbHandler)
        {
            _cosmosDbHandler = cosmosDbHandler;
        }
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            var customer = await _cosmosDbHandler.GetMany<Customer>(nameof(Customer));
            return View(customer);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if(customer is null)
                throw new ArgumentNullException(nameof(customer));
            customer.CreateDate = DateTime.Now;

            _cosmosDbHandler.Insert<Customer>(customer);
            return RedirectToAction("index");
        }
        //comment

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var customers = await _cosmosDbHandler.GetMany<Customer>(nameof(Customer));
            var customer=customers.FirstOrDefault(m=>m.Id==id);
            _cosmosDbHandler.Remove<Customer>(customer);

            return RedirectToAction("Index");
        }

        // POST: CustomerController/Delete/5
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
