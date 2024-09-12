using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PLL.Interfaces;

namespace MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepositry EmployeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepositry EmployeeRepository, IWebHostEnvironment _env)
        {
            this.EmployeeRepository = EmployeeRepository;
            this._env = _env;
        }
        public IActionResult Index()
        {
           var employee= EmployeeRepository.GetAll();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
               var count= EmployeeRepository.Add(employee);
                if (count>0)
                {
                   return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }
        
        [HttpGet]
        public IActionResult Details(int? id,string viewName= "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee = EmployeeRepository.GetById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(viewName, employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}
            //var department = departmentRepository.GetById(id.Value);
            //if (department == null)
            //{
            //    return NotFound();
            //}
            //return View(department);
            return Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id,Employee employee)
        {
            if (id!= employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            return View(employee);

            try 
            {
                var count = EmployeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(System.Exception e) 
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed To Update");
                }
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}
            //var department = departmentRepository.GetById(id.Value);
            //if (department == null)
            //{
            //    return NotFound();
            //}
            //return View(department);
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                var count = EmployeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));
            }
            catch (System.Exception e)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed To Delete");
                }
            }
            return View(employee);
        }
    }
}
