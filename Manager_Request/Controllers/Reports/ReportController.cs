using Manager_Request.Application.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.Reports
{
    public class ReportController:BaseApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReportUsers()
        {
            return Ok(await _service.ReportUsers());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReportTasks()
        {
            return Ok(await _service.ReportTask());
        }
    }
}
