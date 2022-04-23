using Manager_Request.Application.Services.NoteTasks;
using Manager_Request.Application.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.NoteTasks
{
    public class NoteTaskController : BaseApiController
    {
        private readonly INoteTaskService _service;

        public NoteTaskController(INoteTaskService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] NoteTaskViewModel model)
        {
            return Ok(await _service.AddAsync(model));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int key)
        {
            return Ok(await _service.DeleteAsync(key));
        }
    }
}
