using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PLL.Interfaces;
using MVC_03.PLL.Repositries;

namespace MVC_03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository,IWebHostEnvironment _env)
        {
            this.departmentRepository= departmentRepository;
            this._env = _env;
        }
        public IActionResult Index()
        {
           var departments=departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
               var count= departmentRepository.Add(department);
                if (count>0)
                {
                   return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
        
        [HttpGet]
        public IActionResult Details(int? id,string viewName= "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department=departmentRepository.GetById(id.Value);
            if (department==null)
            {
                return NotFound();
            }
            return View(viewName,department);
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
        public IActionResult Edit([FromRoute] int? id,Department department)
        {
            if (id!=department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            return View(department);

            try 
            {
                var count = departmentRepository.Update(department);
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
            return View(department);
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
        public IActionResult Delete(Department department)
        {
            try
            {
                var count = departmentRepository.Delete(department);
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
            return View(department);
        }
    }
}
