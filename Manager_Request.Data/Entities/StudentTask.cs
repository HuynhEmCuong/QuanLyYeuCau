﻿using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manager_Request.Data.Entities
{
   public class StudentTask
    {
        [Key]
        public int RequestId { get; set; }

        [Key]
        public int StudentId { get; set; }

        public int? ReceiverId { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public RequestStatus Status { get; set; }

        public DateTime? Finish_date { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? AssignDate { get; set; }

        [ForeignKey(nameof(RequestId))]
        public virtual RequestType RequestType { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public virtual AppUser AppUser { get; set; }
    }

}
