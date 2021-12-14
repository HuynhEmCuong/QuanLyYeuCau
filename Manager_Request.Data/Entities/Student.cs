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
        public string StudenId { get; set; }

        public virtual ICollection<TaskList> TaskLists { get; set; }

    }
}
