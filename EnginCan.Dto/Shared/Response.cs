using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Dto.Shared
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public Messages Error { get; set; }
        public Messages Success { get; set; }

        public Response(bool isSuccess, string message) => Creator(isSuccess, message, null, null);

        public Response(bool isSuccess, string message, int? time) => Creator(isSuccess, message, time, null);

        public Response(bool isSuccess, string message, int? time, string code) => Creator(isSuccess, message, time, code);

        private void Creator(bool isSuccess, string message, int? time, string code)
        {
            IsSuccess = isSuccess;
            if (isSuccess)
                Success = new Messages(message, time, code);
            else
                Error = new Messages(message, time, code);
        }
    }

    public class Messages
    {
        public Messages(string message, int? time, string code)
        {
            Message = message;
            Time = time;
            Code = code;
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public int? Time { get; set; }
    }
}
