﻿using Manager_Request.Data.Enums;
using Manager_Request.Data.Enums.RequestType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.ViewModels
{
   public class RequestTypeViewModel
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public int? ExecutionTime { get; set; }

        public RequestTypeStatus StatusRequest { get; set; }

        public RequestTypeUpload IsUpload { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
