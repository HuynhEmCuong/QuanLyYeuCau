using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manager_Request.Data.Entities
{
    public class NoteTask
    {
        [Key]
        public int Id { get; set; }

        public int StudentTaskId { get; set; }

        public int UserNoteId { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(512)]
        public string Note { get; set; }


        [ForeignKey(nameof(UserNoteId))]
        public virtual AppUser UserNote { get; set; }

        [ForeignKey(nameof(StudentTaskId))]
        public virtual StudentTask StudentTask { get; set; }


    }
}
