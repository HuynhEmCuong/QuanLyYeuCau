using Manager_Request.Application.Jobs;
using Manager_Request.Application.Jobs.JobSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Installers
{
    public class JobInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<SendMailNotifiUserJob>();
            services.AddSingleton(new JobSchedule(
                    jobType: typeof(SendMailNotifiUserJob),
                    cronExpression: "0 0/15 * ? * 1-5")); // mỗi 15giay

            //services.AddSingleton(new JobSchedule(
            //       jobType: typeof(SendMailNotifiUserJob),
            //       //cronExpression: "0 0 8 ? * 1-5")); // vào 8 giờ từ t2 -> t6
        }
    }
}
