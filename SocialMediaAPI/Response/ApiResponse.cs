using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.API.Response
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data{ get; set; }

    }
}
