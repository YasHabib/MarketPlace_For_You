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
//using System.Web.Http.Cors;

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
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Controller for user
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="data">User data</param>
        /// <returns>Creates a user and writes the info in databse</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpPost]
        public async Task<ActionResult<UserVM>> Create([FromBody] UserAddVM src)
        {
            try
            {
                // Have the service create the new user
                var result = await _userService.Create(src);

                //Welcome email
                //var apiKey = Environment.GetEnvironmentVariable("SendGridAPIKey"); //Returns a 400 bad request with Value cannot be null. (Parameter 'apiKey') but adds the user
                //var apiKey = _configuration.GetValue<string>("SendGridAPIKey"); //gives back a 200 but no welcome email.
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("yasin_habib@outlook.com", "Market For You");
                //var subject = "Welcome to Market For You";
                //string fullName = src.FirstName + " " + src.LastName;
                //var to = new EmailAddress(src.Email, fullName);
                //var plainTextContent = "and easy to do anywhere, even with C#";
                //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                // Return a 200 response with the userVM
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
