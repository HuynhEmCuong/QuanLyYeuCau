using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manager_Request.Data.Entities
{
   public class Student
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string  FullName { get; set; }

        public DateTime? Birthday{ get; set; }

        [StringLength(255)]
        public string CMND { get; set; }

        public Gender Gender { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string StudentId { get; set; }

        [StringLength(255)]
        public string Mobi { get; set; }

        public Status Status { get; set; }

        public int?  DepartId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<StudentTask> TaskLists { get; set; }

        [ForeignKey(nameof(DepartId))]
        public virtual Department Department { get; set; }

    }
}
