using Manager_Request.Application.Const;
using Manager_Request.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Request.Application.Extensions
{
    public static class ErrorUtil
    {
        public static OperationResult GetMessageError(this Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += " \n " + ex.InnerException.Message;
            }

            return new OperationResult { StatusCode = StatusCode.InternalServerError, Message = message, Success = false };

        }
    }
}
