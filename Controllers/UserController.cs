using Abstraction.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
       

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult>  Post([FromBody] User user)
        {
            ResponsePayload<User> response = new ResponsePayload<User>();
            User existinguser = await _userService.CheckIfUserIsPresent(user.UserEmail);
            if (existinguser == null)
            { 
                 try
                {
                    response.StatusCode = 200;
                    response.Message = await _userService.AddUserToDatabase(user);
                }
                catch (Exception exception)
                {
                    response.StatusCode = 400;
                    response.Message = "User could not be added to database due to " + exception.Message;
                }
            }
            else
            {
                response.StatusCode = 200;
                response.DataList.Add(existinguser);
                response.Message = "The user is already registered with the application";
            }
            return Ok(response);

        }

    }
}
