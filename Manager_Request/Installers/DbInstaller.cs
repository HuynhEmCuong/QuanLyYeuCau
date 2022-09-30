using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLHB.Data.EF;
using Manager_Request.Data.Entities;
using System;

namespace Manager_Request.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //var connetionString = configuration.GetConnectionString("DefaultConnection");

            var connetionString = Environment.GetEnvironmentVariable("CONNECT_STRING_API");

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                connetionString,
                o => o.MigrationsAssembly("Manager_Request.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
