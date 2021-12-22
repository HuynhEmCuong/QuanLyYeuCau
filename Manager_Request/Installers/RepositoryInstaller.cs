using Manager_Request.Data.EF;
using Manager_Request.Data.EF.Interface;
using Manager_Request.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manager_Request.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Student>, EFRepository<Student>>();
            services.AddScoped<IRepository<StudentTask>, EFRepository<StudentTask>>();
            services.AddScoped<IRepository<RequestType>, EFRepository<RequestType>>();
        }
    }
}
