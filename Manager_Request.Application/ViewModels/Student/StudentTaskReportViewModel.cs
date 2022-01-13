using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Student
{
    public class StudentTaskReportViewModel
    {
        public int Received { get; set; }

        public int ReceivedInDay { get; set; }

        public int Doing { get; set; }

        public int Complete { get; set; }

        public int Disbaled { get; set; }

    }
}
