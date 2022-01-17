using Manager_Request.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels
{
   public class StudentViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string StudentId { get; set; }

        public int? DepartId { get; set; }

        public string Mobi { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }


    }
}
