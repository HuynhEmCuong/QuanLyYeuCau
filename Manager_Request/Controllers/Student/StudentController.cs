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
    public class StudentController :BaseApiController
    {
        private readonly  IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> CheckUserExist(string email) => Ok(await _service.CheckUserExist(email));
       

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentViewModel model) => Ok(await _service.AddAsync(model));


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateStuden([FromBody] StudentViewModel model) => Ok(await _service.UpdateAsync(model));


    }
}
