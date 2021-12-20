using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.Const
{
    public class StatusCode
    {
        public static int Ok = 200;
        public static int NoContent = 204;
        public static int BadRequest = 400;
        public static int Forbidden = 403;
        public static int NotFound = 404;
        public static int InternalServerError = 500;
    }
}
