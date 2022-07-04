using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// Controller for listing apis
    /// </summary>
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
        /// Updates a listing
        /// </summary>
        /// <param name="data">Listing data</param>
        /// <returns>Returns the updated listing to database</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpPut]
        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data)
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
        /// <param name="category">Encoded: 
        /// Cars & Vehicle: Cars%20%26%20Vehicle
        /// Real Estate: Real%20Estate</param>
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
        /// Seach function
        /// </summary>
        /// <param name="searchString">Listing data</param>
        /// <returns>Return a listing by id</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<List<ListingVM>>> Search(string searchString)
        {
            try
            {
                // Get the Game entities from the service
                var results = await _listingService.Search(searchString);

                // Return a 200 response with the GameVMs
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "There are no listing associated with {0}",searchString});
            }
        }
        /// <summary>
        /// API for searching while both city and category filter is active. This will not work if one of the filters are null
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="city"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("all/{searchString}/{city}/{category}")]
        public async Task<ActionResult<List<ListingVM>>> SearchWithFilters(string searchString, string city, string category)
        {
            try
            {
                // Get the Game entities from the service
                var results = await _listingService.SearchWithFilters(searchString, city, category);

                // Return a 200 response with the GameVMs
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "There are no listing associated with {0}", searchString });
            }
        }

        /// <summary>
        /// Get a listing by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <param name="userid"> This user ID will be the id of the logged in user, so their created listing will not be displayed on Deals for you and More Deals for you</param>
        /// <returns></returns>
        [HttpGet("deals/{userid}")]
        public async Task<ActionResult<List<ListingVM>>> Deals(string userid)
        {
            try
            {
                // Get the listing entities from the service
                var results = await _listingService.Deals(userid);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Making a purchase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("purchase")]
        public async Task<ActionResult<ListingVM>> Purchase([FromBody] ListingPurchaseVM data)
        {
            var buyerId = User.GetId();
            if (buyerId == null)
            {
                return BadRequest("Invalid Request");
            }
            try
            {
                // Update Listing entity from the service
                var result = await _listingService.Purchase(data, buyerId);

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

 
    }
}
