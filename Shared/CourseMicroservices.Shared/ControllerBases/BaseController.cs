using CourseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMicroservices.Shared.ControllerBases
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        public IActionResult CreateActionResult<T>(ResponseDto<T> response) where T : class
        {
            //if (response.StatusCode==StatusCodes.Status204NoContent)
            //{
            //    return new ObjectResult(null)
            //    {
            //        StatusCode = response.StatusCode,       
            //    };
            //}
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };

        }
    }
}
