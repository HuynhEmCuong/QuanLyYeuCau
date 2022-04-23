using Manager_Request.Application.Dtos.Student;
using Manager_Request.Application.Services.Students;
using Manager_Request.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.Student
{
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckUserExist([FromBody] LoginStudentDto model) => Ok(await _service.CheckUserExist(model));


        [HttpGet]
        public async Task<ActionResult> GetAll() => Ok(await _service.GetAllAsync());


        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentViewModel model) => Ok(await _service.AddAsync(model));


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentViewModel model) => Ok(await _service.UpdateAsync(model));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ImportStudent(IFormFile file) => Ok(await _service.ImportStudent(file));

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetStudentByStudentId(string mssv) => Ok(await _service.GetStudentByMSSV(mssv));
        
    }
}
