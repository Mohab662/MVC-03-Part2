using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class GenaricRepository<T> : IGenaricRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext dpContext; // NULL
        public GenaricRepository(AppDbContext appDbContext)
        {
            // dpContext = new AppDbContext();
            this.dpContext = appDbContext;
        }
        public int Add(T item)
        {
            dpContext.Add(item);
            return dpContext.SaveChanges();
        }

        public int Delete(T item)
        {
            dpContext.Remove(item);
            return dpContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>)dpContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            }
            else
            {
                return dpContext.Set<T>().AsNoTracking().ToList();
            }

        }

        public T GetById(int id)
        {
            return dpContext.Set<T>().Find(id);
        }

        public int Update(T item)
        {
            dpContext.Set<T>().Update(item);
            return dpContext.SaveChanges();
        }
    }
}
