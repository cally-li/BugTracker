using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        
        //401 Unauthorized
        [Authorize]
        [HttpGet("auth")]  //api/buggy/auth
        public ActionResult<string> GetSecret()
        {
            return "You are unauthorized";
        }
        
        //404 Not Found
        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
            var notUser = _context.Users.Find(-1); //will return null user
            if (notUser == null) return NotFound(); 
            return Ok(notUser); 
        }

        //500 Internal Server Error
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            
            var nullObject = _context.Users.Find(-1); // = null

            //to test the exception middleware, generate a null reference exception (by executing a method on a null obj) 
            var objectToReturn = nullObject.ToString(); 

            return objectToReturn; 
        }

        //400 Bad Request
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was a bad request");
        }

    }
}
