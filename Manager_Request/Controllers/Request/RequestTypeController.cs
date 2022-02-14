using Manager_Request.Application.Configuration;
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

        [HttpGet]
        public async Task<ActionResult> FindByIdAsync(int id)
        {
            return Ok(await _service.FindByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] RequestTypeViewModel model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] RequestTypeViewModel model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int key)
        {
            return Ok(await _service.DeleteAsync(key));
        }

        [HttpGet]
        public async Task<ActionResult> LoadDxoGridAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(await _service.LoadDxoGridAsync(loadOptions));
        }

        [HttpGet]
        public async Task<ActionResult> LoadDxoLookupAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(await _service.LoadDxoLookupAsync(loadOptions));
        }
    }
}
