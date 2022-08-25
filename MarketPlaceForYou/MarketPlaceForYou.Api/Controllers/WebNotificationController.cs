using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using MarketPlaceForYou.Services.Services;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{/// <summary>
/// Controller for notifications
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class WebNotificationController : ControllerBase
    {
        private IUserService _userService;
        private IEmailService _emailService;
        private IWebNotificationService _webNotificationService;
        /// <summary>
        /// Construstor for email sending
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="emailService"></param>
        public WebNotificationController(IUserService userService, IEmailService emailService, IWebNotificationService webNotificationService)
        {
            _userService = userService;
            _emailService = emailService;
            _webNotificationService = webNotificationService;  
        }

        /// <summary>
        /// Retrieving the total amount of 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> PendingListingCount()
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");

                var result = await _webNotificationService.PendingListingCount(userId);

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
