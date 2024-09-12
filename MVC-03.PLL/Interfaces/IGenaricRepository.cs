using MVC_03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Interfaces
{
    public interface IGenaricRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T employee);
        int Update(T employee);
        int Delete(T employee);
    }
}
