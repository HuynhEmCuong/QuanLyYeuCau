using Manager_Request.Application.Services.Request;
using Manager_Request.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.Request
{
    public class RequestTypeController:BaseApiController
    {
        private IRequestTypeService _service;

        public RequestTypeController(IRequestTypeService service)
        {
            _service = service;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll() => Ok(await _service.GetAllAsync());
    }
}
