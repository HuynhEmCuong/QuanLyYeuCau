using Manager_Request.Data.Enums;
using Manager_Request.Data.Enums.RequestType;
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

        
        [StringLength(255)]
        public string  Name { get; set; }

        [StringLength(255)]
        public string  Description { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public int? ExecutionTime { get; set; }

        public Status Status { get; set; }

        public RequestTypeStatus StatusRequest { get; set; }

        public int SortOrder { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

    }
}
