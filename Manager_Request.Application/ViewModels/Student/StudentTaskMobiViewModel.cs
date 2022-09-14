using Manager_Request.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Student
{
    public class StudentTaskMobiViewModel
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int StudentId { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public IFormFile File { get; set; }

        public RequestStatus Status { get; set; }
        public string FileNameStudent { get; set; }
        public string FilePathStudent { get; set; }

    }
}
