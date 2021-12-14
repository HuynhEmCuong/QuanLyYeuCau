using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Manager_Request.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Manager_Request.Data.Entities
{
    [Table("AppUsers")]
    public  class AppUser: IdentityUser<int>
    {
        [StringLength(255)]
        public string Address { get; set; }

        public string Description { get; set; }
        public int? Position { get; set; }
        public Status Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Name { get; set; }
    }
}
