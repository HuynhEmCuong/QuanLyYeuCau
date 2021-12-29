using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manager_Request.Data.Entities
{
   public class Student
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string  FullName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(10)]
        public string StudentId { get; set; }

        [StringLength(11)]
        public string Mobi { get; set; }

        public Status Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<StudentTask> TaskLists { get; set; }

    }
}
