using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.Dtos.Student
{
    public class StudentTaskMobiParmsDtos
    {
        public string Email { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
