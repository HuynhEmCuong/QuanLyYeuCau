using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manager_Request.Data.Entities
{
    public class RequestType
    {
        [Key]
        public int Id { get; set; }

        public string  Name { get; set; }

        public string  Description { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

    }
}
