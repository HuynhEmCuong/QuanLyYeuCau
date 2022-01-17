using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manager_Request.Data.Entities
{
    public class EmailLog
    {
        public int  Id { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        public int Sender { get; set; }


        public string Receiver { get; set; }

        public TypeEmail Type{ get; set; }

        public DateTime CreateDate { get; set; }


        [ForeignKey(nameof(Sender))]
        public virtual AppUser AppUser { get; set; }


    }
}
