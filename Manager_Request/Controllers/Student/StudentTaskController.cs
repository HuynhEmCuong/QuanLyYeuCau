﻿using Manager_Request.Application.Configuration;
using Manager_Request.Application.Dtos.Student;
using Manager_Request.Application.Services.Students;
using Manager_Request.Application.ViewModels;
using Manager_Request.Application.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.Student
{
    public class StudentTaskController : BaseApiController
    {
        private IStudentTaskService _service;

        public StudentTaskController(IStudentTaskService service)
        {
            _service = service;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserTask([FromBody] StudentTaskViewModel data) => Ok(await _service.AddAsync(data));


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByStudentId(int id) => Ok(await _service.GetListTaskByStudentId(id));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByStudentEmail([FromBody] StudentTaskMobiParmsDtos model) => Ok(await _service.GetListTaskByEmail(model));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddTaskFromMobi([FromForm] StudentTaskMobiViewModel data) => Ok(await _service.AddTaskFromMobie(data));


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTask(int id) => Ok(await _service.DeleteAsync(id));


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

        [HttpGet]
        public async Task<IActionResult> FindByIdIncludeAsync(int id)
        {
            return Ok(await _service.GetTaskInclude(id));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] StudentTaskViewModel data)
        {
            return Ok(await _service.UpdateAsync(data));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNoteAsync([FromBody] StudentTaskViewModel data)
        {
            return Ok(await _service.UpdateNoteUser(data));
        }

        [HttpPut]
        public async Task<IActionResult> ChangeTaskForUser([FromBody] StudentTaskViewModel data)
        {
            return Ok(await _service.ChangeTaskForUser(data));
        }

        

        [HttpGet]
        public async Task<IActionResult> CheckTaskOfUser(int userId, int taskId)
        {
            return Ok(await _service.CheckTaskOfUser(userId, taskId));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AutoSendMail()
        {
            await _service.AutoSendMailNotifiTask();
            return NotFound();
        }

    }
}
