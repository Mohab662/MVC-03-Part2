using MVC_03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Interfaces
{
    public interface IEmployeeRepositry : IGenaricRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();
        //Employee GetById(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
        IQueryable<Employee> GetEmployeeByAddress(string address);
    }
}
