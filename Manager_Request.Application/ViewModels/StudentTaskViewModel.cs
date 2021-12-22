using Manager_Request.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels
{
   public class StudentTaskViewModel
    {

        public int RequestId { get; set; }

        public int StudentId { get; set; }

        public int? ReceiverId { get; set; }

        public string Note { get; set; }
        public DateTime Finish_date { get; set; }

        public DateTime Received_date { get; set; }


        public DateTime Modify_Date { get; set; }

        public int? ModifyBy { get; set; }

        public virtual AppUser User { get; set; }

        public virtual RequestType RequestType { get; set; }

        public virtual Student Student { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
