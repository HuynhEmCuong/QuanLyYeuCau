using Manager_Request.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager_Request.Data.Entities
{
    public class StudentTask
    {
        [Key]
        public int Id{ get; set; }
        
        public int RequestId { get; set; }

        public int StudentId { get; set; }

        public int? ReceiverId { get; set; }

        public RequestStatus Status { get; set; }
        //Note for student
        [StringLength(255)]
        public string Note { get; set; }

        //Note for user
        [StringLength(255)]
        public string NoteUser { get; set; }

        public int Quantity { get; set; }

        public DateTime? FinishDate { get; set; } // Thời gian hoàn thành công việc

        public DateTime? AssignDate { get; set; } // Thời gian nhận công việc

        public DateTime? IntendTime { get; set; } // Thời gian dự định hoàn thành công việc

        public DateTime? CreateDate { get; set; } // Thời gian tạo  công việc

        [StringLength(200)]
        public string FileName { get; set; }

        [StringLength(200)]
        public string FilePath { get; set; }

        public DateTime? ModifyDate { get; set; }

        [ForeignKey(nameof(RequestId))]
        public virtual RequestType RequestType { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public virtual AppUser AppUser { get; set; }
    }

}
