using Manager_Request.Data.Enums;
using Manager_Request.Ultilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Student
{
    public class StudentTaskMobiViewModel
    {
        private string _note;

        public int Id { get; set; }

        public int RequestId { get; set; }

        public int StudentId { get; set; }

        //public string Note { get; set; }
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value.ToJsonObject<string>();
            }
        }

        public int Quantity { get; set; }

        public IFormFile File { get; set; }

        public RequestStatus Status { get; set; }
        public string FileNameStudent { get; set; }
        public string FilePathStudent { get; set; }

      

    }
}
