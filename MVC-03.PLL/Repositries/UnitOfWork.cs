using MVC_03.DAL.Data;
using MVC_03.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_03.PLL.Repositries
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            EmployeeRepositry = new EmployeeRepositry(dbContext);
            DepartmentRepository=new DepartmentRepository(dbContext);
            this.dbContext = dbContext;
        }
        public IEmployeeRepositry EmployeeRepositry { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public int Complete()
        {
           return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
