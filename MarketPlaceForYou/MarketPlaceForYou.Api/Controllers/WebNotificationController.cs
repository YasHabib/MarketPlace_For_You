//using MarketPlaceForYou.Api.Helpers;
//using MarketPlaceForYou.Models.Entities;
//using MarketPlaceForYou.Models.ViewModels;
//using MarketPlaceForYou.Models.ViewModels.User;
//using MarketPlaceForYou.Services.Services;
//using MarketPlaceForYou.Services.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace MarketPlaceForYou.Api.Controllers
//{/// <summary>
///// Controller for notifications
///// </summary>
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]

//    public class WebNotificationController : ControllerBase
//    {
//        private IUserService _userService;
//        private IEmailService _emailService;
//        //private IWebNotificationService _webNotificationService;
//        /// <summary>
//        /// Construstor for email sending
//        /// </summary>
//        /// <param name="userService"></param>
//        /// <param name="emailService"></param>
//        public WebNotificationController(IUserService userService, IEmailService emailService, IWebNotificationService webNotificationService)
//        {
//            _userService = userService;
//            _emailService = emailService;
//            _webNotificationService = webNotificationService;
//        }

//        /// <summary>
//        /// Retrieving the total amount of pending listings
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost("pendingCount")]
//        public async Task<ActionResult<int>> PendingListingCount()
//        {
//            try
//            {
//                var userId = User.GetId();
//                if (userId == null)
//                    return BadRequest("Invalid Request");

//                var result = await _webNotificationService.PendingListingCount(userId);

//                return Ok(result);
//            }
//            catch (DbUpdateException)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }

//        }

//        /// <summary>
//        /// 1st notification user get's upon their 1st time login.
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost("welcome/{notificationId}")]
//        public async Task<ActionResult<InAppNotificationVM>> Welcome(string notificationId)
//        {
//            try
//            {

//                var result = await _webNotificationService.WelcomeNotification(notificationId);

//                return Ok(result);
//            }
//            catch (DbUpdateException)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Create your 1st offer
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost("CreateListing/{notificationId}")]
//        public async Task<ActionResult<InAppNotificationVM>> CreateListing(string notificationId)
//        {
//            try
//            {

//                var result = await _webNotificationService.Create1stListing(notificationId);

//                return Ok(result);
//            }
//            catch (DbUpdateException)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Marks all notifications as Read
//        /// </summary>
//        /// <returns></returns>
//        [HttpPut]
//        public async Task<ActionResult> MarkAsRead()
//        {
//            try
//            {
//                await _webNotificationService.MarkAsRead();

//                return Ok();
//            }
//            catch (DbUpdateException)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//    }
//}
