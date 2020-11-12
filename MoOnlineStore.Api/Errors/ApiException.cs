using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoOnlineStore.Api.Errors
{
    public class ApiException : ApiResponse
    {
        public string _details { get; }
        public ApiException(int statusCode, string message = null,
            string details = null) : base(statusCode, message)
        {
            _details = details;
        }
    }
}
