using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestAppPostgre.Models;
using TestAppPostgre.Repository;

namespace TestAppPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository customerRepository;

        public CustomerController(IConfiguration configuration)
        {
            customerRepository = new CustomerRepository(configuration);
        }


        public IActionResult Index()
        {
            return Ok(customerRepository.FindAll());
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(Customer cust)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Add(cust);
                return RedirectToAction("Index");
            }
            return Ok(cust);

        }

        // GET: /Customer/Edit/1
        [HttpGet("Ed")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer obj = customerRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);

        }

        // POST: /Customer/Edit   
        [HttpPost]
        public IActionResult Edit(Customer obj)
        {

            if (ModelState.IsValid)
            {
                customerRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return Ok(obj);
        }

        // GET:/Customer/Delete/1
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            customerRepository.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}