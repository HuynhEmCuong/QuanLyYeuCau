﻿using Manager_Request.Application.Services.DepartService;
using Manager_Request.Application.ViewModels.Department;
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
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(DepartmentViewModel model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(DepartmentViewModel model)
        {
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DepartmentViewModel model)
        {
            return Ok(await _service.DeleteAsync(model));
        }


    }
}
