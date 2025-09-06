using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ANDON_Domain.Exceptions
{
    public class BaseException:Exception
    {
        public int ErrorCode { get; set; }
        public string? DevMessage { get; set; }
        public string? UserMessage { get; set; }
        public object? Errors { get; set; }

        public BaseException() { }

        public BaseException(int errorCode, string userMessage, string devMessage)
            : base(userMessage)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
            DevMessage = devMessage;
        }

        public override string ToString()
        {
            var response = new
            {
                ErrorCode,
                UserMessage,
                DevMessage,
                Errors
            };
            return JsonSerializer.Serialize(response);
        }

    }
}
