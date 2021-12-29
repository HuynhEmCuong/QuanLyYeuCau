using Manager_Request.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels.System
{
    public class AppUserViewModel
    {

        public AppUserViewModel()
        {
            Roles = new List<string>();
        }
        public int Id { get; set; }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }
        public int? Position { get; set; }
        public Status Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Name { get; set; }

        public List<string> Roles { get; set; }
    }
}
