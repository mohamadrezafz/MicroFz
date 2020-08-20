using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime;
using System.Text;
using CoreBase.Api.Response;


namespace MicroFz.Common.Constant
{
    public class FinalResult : HttpBodyResponse
    {
        public FinalResult(string message, string code, HttpStatusCode httpStatus) : base(message,code,httpStatus)
        {
            this.Message = message;
            this.Code = code;
            this.HttpStatus = httpStatus;
        }

    }
}
