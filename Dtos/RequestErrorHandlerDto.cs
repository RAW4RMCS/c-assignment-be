using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Dtos
{
    public class RequestErrorHandlerDto<T>
    {
        public T Status { get; set; }
        public string Message { get; set; }

    }
}
