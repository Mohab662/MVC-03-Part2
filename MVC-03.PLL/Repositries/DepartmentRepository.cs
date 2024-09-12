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
    public class DepartmentRepository :GenaricRepository<Department> ,IDepartmentRepository
    {
    // private  readonly AppDbContext dpContext; // NULL
        public DepartmentRepository(AppDbContext appDbContext):base(appDbContext)
        {
            // dpContext = new AppDbContext();
           // this.dpContext = appDbContext;
        }

    }
}
