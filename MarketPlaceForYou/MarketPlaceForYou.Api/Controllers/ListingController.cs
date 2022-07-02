using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;
        /// <summary>
        /// Controller for listing
        /// </summary>
        /// <param name="listingService"></param>
        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        /// <summary>
        /// Creates a listing
        /// </summary>
        /// <param name="data">Listing data</param>
        /// <returns>Returns the game with an ID</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>

        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingAddVM data)
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }

                // Have the service create the new Listing
                var result = await _listingService.Create(data, userId);

                // Return a 200 response with the ListingVM
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
        /// Returns all the created listings
        /// </summary>
        /// <returns>Returns all listings from databse</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("all")]
        public async Task<ActionResult<List<ListingVM>>> GetAll()
        {
            try
            {
                // Get the listing entities from the service
                var results = await _listingService.GetAll();

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Filters all the listings by city
        /// </summary>
        /// <param name="city">Listing data</param>
        /// <returns>Returns all listings by city</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("all/{city}")]
        public async Task<ActionResult<List<ListingVM>>> GetAllByCity(string city)
        {
            try
            {
                // Get the listing entities from the service
                var results = await _listingService.GetAllByCity(city);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Filters all listings by Category (works but trying to localize it or encode the city name)
        /// </summary>
        /// <param name="category">Listing data</param>
        /// <returns>Returns all listings by category</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("all/category/{category}")]
        public async Task<ActionResult<List<ListingVM>>> GetAllByCategory(string category)
        {
            try
            {
                // Get the listing entities from the service
                var results = await _listingService.GetAllByCategory(category);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Return a listing by their specific id
        /// </summary>
        /// <param name="id">Listing data</param>
        /// <returns>Return a listing by id</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> GetById([FromRoute] Guid id)
        {
            try
            {
                // Get the requested Listing entity from the service
                var result = await _listingService.GetById(id);

                // Return a 200 response with the ListingVM
                return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Unable to retrieve the requested Listing" });
            }
        }

        /// <summary>
        /// Deals for you and More deals for you
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet("deals")]
        public async Task<ActionResult<List<ListingVM>>> Deals(decimal price)
        {
            try
            {
                // Get the listing entities from the service
                var results = await _listingService.Deals(price);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates a listing
        /// </summary>
        /// <param name="data">Listing data</param>
        /// <returns>Returns the updated listing to database</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpPut]
        public async Task<ActionResult <ListingVM>> Update([FromBody] ListingUpdateVM data)
        {
            try
            {
                // Update Listing entity from the service
                var result = await _listingService.Update(data);

                // Return a 200 response with the ListingVM
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
        /// Deletes a listing (I do not think we need this, I had to create it as I had to delete some listings)
        /// </summary>
        /// <param name="id">Listing data</param>
        /// <returns>Deletes a listing</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _listingService.Delete(id);

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
