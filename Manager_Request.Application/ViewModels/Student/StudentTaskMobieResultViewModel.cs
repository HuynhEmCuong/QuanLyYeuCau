using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Student
{
    public class StudentTaskMobieResultViewModel
    {
        public Utilities.Pager PageInfo { get; set; }

        public StudentViewModel StudentInfo { get; set; }
    }
}
