using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceForYou.Api.Controllers
{/// <summary>
/// This houses all API's that are related to User functionality from Admin Panel
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class APUserController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly IUserService _userService;
        /// <summary>
        /// Controller for admin panel which will use listing and user service
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="listingService"></param>
        public APUserController(IListingService listingService, IUserService userService)
        {
            _listingService = listingService;
            _userService = userService;

        }
        /// <summary>
        /// Retrives all user data in a list format
        /// </summary>
        /// <returns></returns>
        [HttpGet("Users")]
        public async Task<ActionResult<List<UserVM>>> GetAll()
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");

                var results = await _userService.GetAll(userId);

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }    }
}
