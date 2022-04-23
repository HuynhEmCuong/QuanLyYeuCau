using Manager_Request.Application.ViewModels.System;
using Manager_Request.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.Student
{
    public class NoteTaskViewModel
    {

        public int Id { get; set; }

        public int StudentTaskId { get; set; }

        public int UserNoteId { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Note { get; set; }


        public virtual AppUserViewModel UserNote { get; set; }

        public virtual StudentTaskViewModel StudentTask { get; set; }


    }
}
