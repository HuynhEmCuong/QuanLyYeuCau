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

        [HttpPost]
        public async Task<IActionResult> UploadMultiFileStudent([FromForm] List<IFormFile> file,string studentName)
        {
            return Ok(await _service.UploadMultiFile(file, studentName));
        }

        [HttpGet]
        public IActionResult RemoveFile(string fileName)
        {
            return Ok(_service.RemoveFile(fileName));
        }

        [HttpPost]
        public IActionResult RemoveAllFile([FromBody] List<string> listFileName)
        {
            return Ok(_service.RemoveListFile(listFileName));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, string requestType)
        {
            return Ok(await _service.UploadFile(file, requestType));
        }
    }
}
