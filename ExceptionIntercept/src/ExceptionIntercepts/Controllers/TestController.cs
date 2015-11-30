using Microsoft.AspNet.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExceptionIntercepts.CustomExceptions;

namespace ExceptionIntercepts.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet("{id}")]
        public Task<string> Post(int? id)
        {
            if(id == 0)
            {
                throw new ValidationException("Missing email address");
            }

            if (id == 1)
            {
                throw new NotFoundException("User not found.");
            }

            if (id == 2)
            {
                throw new UnauthorizedAccessException("No such user exists for login.");
            }

            if (id == 3)
            {
                throw new ForbiddenAccessException("This user cannot access this bank account.");
            }

            // unhandled
            throw new ArgumentNullException(nameof(id));
        }
    }
}
