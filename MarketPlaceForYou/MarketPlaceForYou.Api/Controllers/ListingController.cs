using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// Controller for listing APIs
    /// </summary>
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
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
        /// Returns all the created listings which have not created by the logged in user and have not been purchased. 
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
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.GetAll(userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Filters all the listings by city only (non-purchased and listings were not created by the user)
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
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.GetAllByCity(city, userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Filters all listings by Category only (non-purchased and listings were not created by the user)
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
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.GetAllByCategory(category, userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Standalone search function (non-purchased and listings were not created by the user)
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
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the Game entities from the service
                var results = await _listingService.Search(searchString, userId);

                // Return a 200 response with the GameVMs
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "There are no listing associated with {0}", searchString });
            }
        }
        /// <summary>
        /// API for searching while city and/or category filter is active, or search without any filter.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="city"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<ActionResult<List<ListingVM>>> SearchWithFilters([FromQuery] string searchString, [FromQuery] string? city = null, [FromQuery] string? category = null)
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the Game entities from the service
                var results = await _listingService.SearchWithFilters(searchString, userId, city, category);

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
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the requested Listing entity from the service
                var result = await _listingService.GetById(id, userId);

                // Return a 200 response with the ListingVM
                return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Unable to retrieve the requested Listing" });
            }
        }

        /// <summary>
        /// Deals for you and More deals for you (this will retrieve 16 listings based on low/high price)
        /// </summary>
        [HttpGet("deals")]
        public async Task<ActionResult<List<ListingVM>>> Deals()
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.Deals(userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Gets the active listings user has added
        /// </summary>
        /// <returns></returns>
        [HttpGet("mylistings/active")]
        public async Task<ActionResult<List<ListingVM>>> MyActiveListings()
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.MyActiveListings(userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Listings the user has sold
        /// </summary>
        /// <returns></returns>
        [HttpGet("mylistings/sold")]
        public async Task<ActionResult<List<ListingVM>>> MySoldListings()
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.MySoldListings(userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets a list of purchases the user has made
        /// </summary>
        /// <returns></returns>
        [HttpGet("mypurchases")]
        public async Task<ActionResult<List<ListingVM>>> MyPurchases()
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.MyPurchases(userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Making a purchase (this will set the logged in user's id as a buyer id and update it in DB)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("purchase")]
        public async Task<ActionResult<ListingVM>> Purchase([FromBody] ListingPurchaseVM data)
        {

            try
            {
                var buyerId = User.GetId();
                if (buyerId == null)
                {
                    return BadRequest("Invalid Request");
                }
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
