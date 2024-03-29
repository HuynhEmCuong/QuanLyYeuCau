﻿using Manager_Request.Application.Configuration;
using Manager_Request.Application.Services.DepartService;
using Manager_Request.Application.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.Department
{
    public class DepartmentController:BaseApiController
    {
        private readonly IDepartService _service;

        public DepartmentController(IDepartService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet]
        public async Task<ActionResult> FindByIdAsync(int id)
        {
            return Ok(await _service.FindByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] DepartmentViewModel model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] DepartmentViewModel model)
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
