using Manager_Request.Application.Services.Request;
using Manager_Request.Application.Services.Students;
using Manager_Request.Application.Services.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manager_Request.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentTaskService, StudentTaskService>();
            services.AddScoped<IRequestTypeService, RequestTypeService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
