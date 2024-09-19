using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using MVC_03.DAL.Models;
using MVC_03.PL.Helpers;
using MVC_03.PL.ViewModels;
using MVC_03.PLL.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace MVC_03.PL.Controllers
{
    public class EmployeeController : Controller
    {
       // private readonly IEmployeeRepositry EmployeeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _env;
       // private readonly IDepartmentRepository DepartmentRepository;
        private readonly IMapper Mapper;

        public EmployeeController(IUnitOfWork unitOfWork /*IEmployeeRepositry EmployeeRepository*/, IWebHostEnvironment _env,/*IDepartmentRepository DepartmentRepository,*/IMapper Mapper)
        {
            //this.EmployeeRepository = EmployeeRepository;
            this.unitOfWork = unitOfWork;
            this._env = _env;
           // this.DepartmentRepository = DepartmentRepository;
            this.Mapper = Mapper;
        }
        public IActionResult Index(string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {
                var employee = unitOfWork.EmployeeRepositry.GetAll();
                var mappedEmp = Mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employee);
                return View(mappedEmp);
            }
            else
            {
                var employee = unitOfWork.EmployeeRepositry.GetEmployeeByName(searchInput);
                var mappedEmp = Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
                return View(mappedEmp);
            }

        }
        [HttpGet]
        public IActionResult Create()
        {
           // ViewData["Departments"] = unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
           employeeVM.ImageName= DocumentSettings.UploadFile(employeeVM.Image, "Images");
            var mappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepositry.Add(mappedEmp);
                var count = unitOfWork.Complete();
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
            var employee = unitOfWork.EmployeeRepositry.GetById(id.Value);
            
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
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var mappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                 unitOfWork.EmployeeRepositry.Update(mappedEmp);
                var count = unitOfWork.Complete();
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
                unitOfWork.EmployeeRepositry.Delete(mappedEmp);
                var count = unitOfWork.Complete();
                DocumentSettings.DeleteFile(employeeVM.ImageName,"Images");
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
