using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PL.ViewModels;
using MVC_03.PLL.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepositry EmployeeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IDepartmentRepository DepartmentRepository;
        private readonly IMapper Mapper;

        public EmployeeController(IEmployeeRepositry EmployeeRepository, IWebHostEnvironment _env,IDepartmentRepository DepartmentRepository,IMapper Mapper)
        {
            this.EmployeeRepository = EmployeeRepository;
            this._env = _env;
            this.DepartmentRepository = DepartmentRepository;
            this.Mapper = Mapper;
        }
        public IActionResult Index(string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                var employee = EmployeeRepository.GetAll();
                var mappedEmp = Mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employee);
                return View(mappedEmp);
            }
            else
            {
                var employee = EmployeeRepository.GetEmployeeByName(searchInput);
                var mappedEmp = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
                return View(mappedEmp);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            var mappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (ModelState.IsValid)
            {
               var count= EmployeeRepository.Add(mappedEmp);
                if (count>0)
                {
                   return RedirectToAction(nameof(Index));
                }
            }
            return View(employeeVM);
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
            var mappedEmp = Mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(viewName, mappedEmp);
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
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id!= employeeVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            return View(employeeVM);

            try 
            {
                var mappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var count = EmployeeRepository.Update(mappedEmp);
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
            return View(employeeVM);
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
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                var count = EmployeeRepository.Delete(mappedEmp);
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
            return View(employeeVM);
        }
    }
}
