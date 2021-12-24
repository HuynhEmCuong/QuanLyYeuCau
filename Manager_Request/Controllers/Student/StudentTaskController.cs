using Manager_Request.Application.Services.Students;
using Manager_Request.Application.ViewModels;
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

    }
}
