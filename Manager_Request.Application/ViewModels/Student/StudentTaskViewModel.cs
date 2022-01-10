using Manager_Request.Application.ViewModels.System;
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
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int StudentId { get; set; }

        public int? ReceiverId { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public DateTime? FinishDate { get; set; }

        public DateTime? AssignDate { get; set; }

        public RequestStatus Status { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual RequestTypeViewModel RequestType { get; set; }

        public virtual StudentViewModel Student { get; set; }

        public virtual AppUserViewModel AppUser { get; set; }
    }
}
