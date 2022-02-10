using Manager_Request.Application.ViewModels.Department;
using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manager_Request.Application.ViewModels
{
   public class StudentViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string StudentId { get; set; }

        public string Mobi { get; set; }

        public Status Status { get; set; }

        public int? DepartId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<StudentTaskViewModel> TaskLists { get; set; }

        public virtual DepartmentViewModel Department { get; set; }

    }
}
