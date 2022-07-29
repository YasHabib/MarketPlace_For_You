using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceForYou.Api.Controllers
{/// <summary>
/// Controller for sending user emails
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;
        /// <summary>
        /// Construstor for email sending
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="emailService"></param>
        public EmailController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        /// <summary>
        /// Sending an email
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> SendEmail([FromBody] string email)
        //{
        //    try
        //    {
        //        var results = await _emailService.SendEmail(email);
        //        //"Welcome to MarketForYou", "<p>Now buy stuff" + DateTime.Now + "</p>"
        //        // Return a 200 response
        //        return Ok(results);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        //    }
        //}

    }
}
