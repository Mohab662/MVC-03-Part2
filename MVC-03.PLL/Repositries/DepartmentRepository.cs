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
    public class DepartmentRepository : IDepartmentRepository
    {
     private  readonly AppDbContext dpContext; // NULL
        public DepartmentRepository(AppDbContext appDbContext)
        {
            // dpContext = new AppDbContext();
            this.dpContext = appDbContext;
        }
        public int Add(Department department)
        {
            //dpContext = new AppDbContext(); XXXX
            dpContext.Departments.Add(department);
            return dpContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            dpContext.Departments.Remove(department);
            return dpContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return dpContext.Departments.AsNoTracking().ToList();
            
        }

        public Department GetById(int id)
        {
            // var department = dpContext.Departments.Where(D => D.Id == 10).FirstOrDefault();
            //var department = dpContext.Departments.Local.Where(D => D.Id == 10).FirstOrDefault();
            //if (department==null)
            //{
            //    department = dpContext.Departments.Where(D => D.Id == 10).FirstOrDefault();
            //}
            return dpContext.Departments.Find(id);
           // return dpContext.Departments.Find<Department>(id);
        }

        public int Update(Department department)
        {
            dpContext.Departments.Update(department);
            return dpContext.SaveChanges();
        }
    }
}
