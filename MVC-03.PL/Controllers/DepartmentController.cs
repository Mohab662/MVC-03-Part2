using Microsoft.AspNetCore.Mvc;
using MVC_03.PLL.Interfaces;
using MVC_03.PLL.Repositries;

namespace MVC_03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository= departmentRepository;
        }
        public IActionResult Index()
        {
           var departments=departmentRepository.GetAll();
            return View(departments);
        }
    }
}
