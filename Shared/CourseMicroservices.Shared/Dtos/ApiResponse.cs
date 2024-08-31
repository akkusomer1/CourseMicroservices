using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMicroservices.Shared.Dtos
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public List<string> Errors { get; set; }
    }
}
