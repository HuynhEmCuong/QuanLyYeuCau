using Manager_Request.Application.Dtos;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetReportUsers([FromBody] ParamsDateDto parms)
        {
            return Ok(await _service.ReportUsers(parms));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReportRequest()
        {
            return Ok(await _service.ReportRequest());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ReportTask()
        {
            return Ok(await _service.ReportTask());
        }
    }
}
