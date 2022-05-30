using Manager_Request.Application.Inplementation;
using Manager_Request.Application.Service.SystemService;
using Manager_Request.Application.Services.DepartService;
using Manager_Request.Application.Services.NoteTasks;
using Manager_Request.Application.Services.Reports;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDepartService, DepartService>();

            services.AddScoped<INoteTaskService, NoteTaskService>();

            services.AddScoped<IReportService, ReportService>();
        }
    }
}
