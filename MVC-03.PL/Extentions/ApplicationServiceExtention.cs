using Microsoft.Extensions.DependencyInjection;
using MVC_03.PLL.Interfaces;
using MVC_03.PLL.Repositries;

namespace MVC_03.PL.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static void ApplicationServices(this IServiceCollection services) 
        {
            // services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // services.AddScoped<IEmployeeRepositry, EmployeeRepositry>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
    }
}
