using Manager_Request.Data.Entities;
using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Request.Application.ViewModels
{
   public class StudentTaskViewModel
    {

        public int RequestId { get; set; }

        public int StudentId { get; set; }

        public int? ReceiverId { get; set; }

        public RequestStatus Status { get; set; }

        public string Note { get; set; }
        public DateTime Finish_date { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? AssignDate { get; set; }

        public virtual RequestType RequestType { get; set; }

        public virtual Student Student { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
