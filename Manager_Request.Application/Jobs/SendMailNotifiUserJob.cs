using Manager_Request.Application.Services.Students;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.Jobs
{
    public class SendMailNotifiUserJob : IJob
    {
        private readonly ILogger<SendMailNotifiUserJob> _logger;
        private readonly IServiceProvider _provider;
        private readonly IConfiguration _config;

        public SendMailNotifiUserJob(ILogger<SendMailNotifiUserJob> logger, IServiceProvider provider, IConfiguration config)
        {
            _logger = logger;
            _provider = provider;
            _config = config;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("=============>>Start Job! At {0} <=============", DateTime.Now);
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    try
                    {
                        var now = DateTime.Now;
                        var studentTaskSv = scope.ServiceProvider.GetService<IStudentTaskService>();

                        await studentTaskSv.AutoSendMailNotifiTask();
                        _logger.LogInformation("=============>>Run job succes! At {0} ", DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
