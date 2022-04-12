using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Manager_Request.Application.Service.SystemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Controllers.SystemController
{
    [AllowAnonymous]
    public class FileController : BaseApiController
    {
        private readonly IFileService _service;

        public FileController(IFileService service)
        {
            _service = service;
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadMultiFileStudent([FromForm] List<IFormFile> file,string studentName)
        //{
        //    return Ok(await _service.UploadMultiFile(file, studentName));
        //}

        //[HttpPost]
        //public IActionResult RemoveAllFile([FromBody] List<string> listFileName)
        //{
        //    return Ok(_service.RemoveListFile(listFileName));
        //}

        [HttpGet]
        public IActionResult RemoveFileStudent(string fileName)
        {
            return Ok(_service.RemoveFileStudent(fileName));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileStudent([FromForm] IFormFile file, string requestType)
        {
            return Ok(await _service.UploadFileStudent(file, requestType));
        }
    }
}
