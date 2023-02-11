using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MarketPlaceForYou.Api.Controllers
{/// <summary>
 /// Controller for user related APIs
 /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IWebNotificationService _webNotificationService;

        /// <summary>
        /// Controller for user
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService, IEmailService emailService, IWebNotificationService webNotificationService)
        {
            _userService = userService;
            _emailService = emailService;
            _webNotificationService = webNotificationService;

        }
        
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="src">User data</param>
        /// <returns>Creates a user and writes the info in databse</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpPost]
        public async Task<ActionResult<UserVM>> Create([FromBody] UserAddVM src)
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");
                // Have the service create the new user
                var result = await _userService.Create(src, userId);
                //Sending the welcome email (commented out as SendGrid API key is in AWS Parameter Store and associated AWS account is closed)
                //await _emailService.WelcomeEmail(src.Email, src.FirstName, src.LastName);
                // Return a 200 response with the userVM
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An user with this email already exists" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a user by their Auth id
        /// </summary>
        /// <param name="id">User data</param>
        /// <returns>Returns a user by their id</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVM>> Get([FromRoute] string id)
        {
            try
            {
                // Get the requested user entity from the service
                var result = await _userService.GetById(id);

                // Return a 200 response with the userVM
                return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Unable to retrieve the requested user" });
            }
        }

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="data">User data</param>
        /// <returns>Updates a user and writes the info in databse</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpPut()]
        public async Task<ActionResult<User>> Update([FromBody] UserUpdateVM data)
        {
            try
            {
                // Update user entity from the service
                var result = await _userService.Update(data);

                // Return a 200 response
                return Ok(result);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
