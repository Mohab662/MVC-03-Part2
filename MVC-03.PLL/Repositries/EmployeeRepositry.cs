using Microsoft.EntityFrameworkCore;
using MVC_03.DAL.Data;
using MVC_03.DAL.Models;
using MVC_03.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Repositries
{
    internal class EmployeeRepositry : IEmployeeRepositry
    {
        private readonly AppDbContext dpContext; // NULL
        public EmployeeRepositry(AppDbContext appDbContext)
        {
            // dpContext = new AppDbContext();
            this.dpContext = appDbContext;
        }
        public int Add(Employee employee)
        {
            dpContext.Employees.Add(employee);
            return dpContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            dpContext.Employees.Remove(employee);
            return dpContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return dpContext.Employees.AsNoTracking().ToList();
        }

        public Employee GetById(int id)
        {
            return dpContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            dpContext.Employees.Update(employee);
            return dpContext.SaveChanges();
        }
    }
}
