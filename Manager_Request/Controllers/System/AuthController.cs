using Manager_Request.Application.Dtos;
using Manager_Request.Application.Services.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.System
{
    public class AuthController: BaseApiController
    {
        private IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            return StatusCodeResult(await _service.LoginAsync(model));
        }
    }
}
