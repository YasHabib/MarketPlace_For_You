using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// Controller to host all admin panel listing related APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class APListingsController : ControllerBase
    {
        private readonly IAPListingService _apListingService;

        /// <summary>
        /// Constructor for AP listing controller
        /// </summary>
        /// <param name="apListingService"></param>
        public APListingsController(IAPListingService apListingService)
        {
            _apListingService = apListingService;
        }

        /// <summary>
        /// This API will retrieve all listings where status = Active
        /// </summary>
        /// <returns></returns>
        [HttpGet("Actives")]
        public async Task<ActionResult<List<ListingVM>>> ActiveListings()
        {
            try
            {
                var results = await _apListingService.ActiveListings();

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// This API will retrieve all listings where status = Pending
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pendings")]
        public async Task<ActionResult<List<ListingVM>>> PendingListings()
        {
            try
            {
                var results = await _apListingService.PendingListings();

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// This API will search listings that are active but not pending
        /// </summary>
        /// <returns></returns>
        [HttpGet("Actives/{searchString}")]
        public async Task<ActionResult<List<ListingVM>>> SearchActives(string searchString)
        {
            try
            {
                var results = await _apListingService.SearchActives(searchString);

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// This API will search listings that are pending but not active
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pendings/{searchString}")]
        public async Task<ActionResult<List<ListingVM>>> SearchPendings(string searchString)
        {
            try
            {
                var results = await _apListingService.SearchPendings(searchString);

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// API for admin to approve a pending listing
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("{Id}/approve")]
        public async Task<ActionResult<ListingVM>> ApproveListing(Guid Id)
        {
            try
            {
                var results = await _apListingService.ApproveListing(Id);

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// API for admin to reject a pending listing
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("{Id}/reject")]
        public async Task<ActionResult<ListingVM>> RejectListing(Guid Id)
        {
            try
            {
                var results = await _apListingService.RejectListing(Id);

                // Return a 200 response
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// API for soft deleting a active listing
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut("{Id}/delete")]
        public async Task<ActionResult> SoftDeleteListing(Guid Id)
        {
            try
            {
                await _apListingService.SoftDelete(Id);

                // Return a 200 response
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
