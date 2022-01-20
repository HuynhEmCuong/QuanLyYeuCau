using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_Request.Application.Configuration
{
    public class MailOptions
    {
        public string  Port { get; set; }

        public string Host { get; set; }

        public string EnableSSl { get; set; }
    }
}
