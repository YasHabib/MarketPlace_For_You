using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Models.ViewModels.User;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{/// <summary>
/// This houses all API's that are related to User functionality from Admin Panel
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class APUsersController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly IUserService _userService;
        /// <summary>
        /// Controller for admin panel which will use listing and user service
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="listingService"></param>
        public APUsersController(IListingService listingService, IUserService userService)
        {
            _listingService = listingService;
            _userService = userService;

        }
        /// <summary>
        /// Retrives all user data in a list format
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<List<APUserListVM>>> GetAll()
        {
            try
            {
                //var userId = User.GetId();
                //if (userId == null)
                //    return BadRequest("Invalid Request");

                var results = await _userService.GetAll();

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Getting user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<APUserListVM>> APGetById(string id)
        {
            try
            {
                var results = await _userService.APGetById(id);

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Gets the user's active listing by the user Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/active")]
        public async Task<ActionResult<List<ListingVM>>> UsersActiveListings(string id)
        {

            try
            {
                // Get the listing entities from the service
                var results = await _listingService.MyActiveListings(id);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Gets the user's active listing by the user Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/purchased")]
        public async Task<ActionResult<List<ListingVM>>> UsersPurchasedListings(string id)
        {

            try
            {
                // Get the listing entities from the service
                var results = await _listingService.MyPurchases(id);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Soft deleting an user (not fully implemented)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> SoftDelete([FromRoute] string id)
        //{
        //    try
        //    {
        //        await _userService.SoftDelete(id);

        //        // Return a 200 response
        //        return Ok();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
        //    }
        //    catch
        //    {
        //        return BadRequest(new { message = "Unable to delete the requested Listing" });
        //    }
        //}
        /// <summary>
        /// Blocks the user (not fully implemented)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}/block")]
        public async Task<ActionResult> BlockUser([FromRoute] string id)
        {
            try
            {
                await _userService.BlockUser(id);

                // Return a 200 response
                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
            }
            catch
            {
                return BadRequest(new { message = "Unable to delete the requested Listing" });
            }
        }
        /// <summary>
        /// Unblocks the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/unblock")]
        public async Task<ActionResult> UnblockUser([FromRoute] string id)
        {
            try
            {
                await _userService.UnblockUser(id);

                // Return a 200 response
                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
            }
            catch
            {
                return BadRequest(new { message = "Unable to delete the requested Listing" });
            }
        }
    }
}
