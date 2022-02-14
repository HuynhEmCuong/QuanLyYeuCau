using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Manager_Request.Controllers;
using Manager_Request.Application.Services.System;
using Manager_Request.Application.ViewModels.System;
using Manager_Request.Utilities;
using Manager_Request.Application.Configuration;

namespace Manager_Request.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] AppUserViewModel model)
        {
            return StatusCodeResult(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] AppUserViewModel model)
        {
            return StatusCodeResult(await _service.UpdateAsync(model));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int key)
        {
            return StatusCodeResult(await _service.DeleteAsync(key));
        }

        [HttpGet]
        public async Task<ActionResult> FindByIdAsync(int id)
        {
            return Ok(await _service.FindByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> PaginationAsync(ParamaterPagination paramater)
        {
            return Ok(await _service.PaginationAsync(paramater));
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

        [HttpPost]
        public async Task<ActionResult> Validate([FromBody] AppUserViewModel model)
        {
            return Ok(await _service.ValidateAsync(model));
        }

    }
}