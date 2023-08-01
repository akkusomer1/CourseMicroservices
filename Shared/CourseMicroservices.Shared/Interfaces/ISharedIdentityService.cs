using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMicroservices.Shared.Interfaces
{
    public interface ISharedIdentityService
    {
        string GetUserId { get; }
    }
}
