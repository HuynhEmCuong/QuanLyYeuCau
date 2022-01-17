using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Department
{
  public class DepartmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
