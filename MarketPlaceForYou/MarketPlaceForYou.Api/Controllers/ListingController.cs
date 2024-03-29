﻿using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MarketPlaceForYou.Models.Entities.Listing;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// Controller for listing APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly IEmailService _emailService;
        /// <summary>
        /// Controller for listing
        /// </summary>
        /// <param name="listingService"></param>
        public ListingController(IListingService listingService, IEmailService emailservice)
        {
            _listingService = listingService;
            _emailService = emailservice;
        }

        /// <summary>
        /// Creates a listing
        /// </summary>
        /// <param name="data">Listing data</param>
        /// <returns>Returns the listing with an ID</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        //------------------------------------------------------------CREATE------------------------------------------------

        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingAddVM data, Categories category, Cities city, Conditions condition)
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");

                // Have the service create the new Listing
                var result = await _listingService.Create(data, userId, category, city, condition);

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
        //------------------------------------------------------------UPDATE------------------------------------------------

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
                    return BadRequest("Invalid Request");
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
        /// <param name="city">Calgary/Canmore/Brooks</param>
        /// <returns>Returns all listings by city</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpGet("all/{city}")]
        public async Task<ActionResult<List<ListingVM>>> GetAllByCity(Cities city)
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.GetAllByCity(city,userId);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Filter by categories
        /// </summary>
        /// <param name="category">
        /// Cars & Vehicle
        /// Electronics
        /// Real Estate
        /// Furniture
        /// </param>
        /// <returns></returns>
        [HttpGet("all/category/{category}")]
        public async Task<ActionResult<List<ListingVM>>> GetAllByCategory(Categories category)
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");

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
                    return BadRequest("Invalid Request");
                // Get the listing entities from the service
                var results = await _listingService.Search(searchString, userId);

                // Return a 200 response with the listingVMs
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "There are no listings associated with "+ searchString + " ."});
            }
        }
        /// <summary>
        /// API for searching while city and/or category filter is active, or search without any filter.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="city"></param>
        /// <param name="category"></param>
        /// <param name="condition"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<ActionResult<List<ListingVM>>> SearchWithFilters([FromQuery] string? searchString=null, [FromQuery] Cities? city = null, [FromQuery] Categories? category = null, [FromQuery] Conditions? condition = null, [FromQuery] decimal minPrice=0, [FromQuery]decimal maxPrice=0)
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");
                if (minPrice > maxPrice)
                    return BadRequest("Invalid Price");
                // Get the listing entities from the service
                var results = await _listingService.SearchWithFilters(userId, searchString, city, category, condition, minPrice, maxPrice);

                // Return a 200 response with the listingVMs
                return Ok(results);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "No results found"});
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
        /// Deals for you and More deals for you (this will retrieve 16 listings based on low/high price)
        /// </summary>
        [HttpGet("deals")]
        public async Task<ActionResult<List<ListingVM>>> Deals()
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");

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
        //User Listings
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
                    return BadRequest("Invalid Request");
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
        /// List of pending purchase requests coming from a buyers to the lister
        /// </summary>
        /// <returns></returns>
        [HttpGet("mylistings/pendings")]
        public async Task<ActionResult<List<ListingVM>>> PendingListings()
        {

            try
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Get the listing entities from the service
                var results = await _listingService.PendingListings(userId);


                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        //------------------------------------------------------------UPDATE------------------------------------------------

        //Purchases
        /// <summary>
        /// Confirming the purchase from pending purchase by the lister.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("confirmpurchase")]
        public async Task<ActionResult<ListingVM>> ConfirmPurchase([FromBody] ListingPurchaseVM data)
        {

            try
            {
                // Update Listing entity from the service
                var result = await _listingService.ConfirmPurchase(data);

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
        /// Buyer sents a request for the lister to confirm their purchase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("requestpurchase")]
        public async Task<ActionResult<ListingVM>> RequestPurchase([FromBody] ListingPurchaseVM data)
        {

            try
            {
                var buyerId = User.GetId();
                if (buyerId == null)
                {
                    return BadRequest("Invalid Request");
                }
                // Update Listing entity from the service
                var result = await _listingService.RequestPurchase(data, buyerId);
                //Sending an email notification to the user who created the listing
                await _emailService.PendingListing(data, buyerId);

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
        /// Lister cancelling a purchase after a buyer has requested (pending purchase) a listing to purchase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("cancelpurchase")]
        public async Task<ActionResult<ListingVM>> CancelPurchase([FromBody] ListingPurchaseVM data)
        {

            try
            {
                // Update Listing entity from the service
                var result = await _listingService.CancelPurchase(data);

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
        /// <param name="category"></param>
        /// <param name="city"></param>
        /// <param name="condition"></param>
        /// <returns>Returns the updated listing to database</returns>
        /// <response code = "200">Successfull</response>
        /// <response code = "401">User not logged in or token has expired</response>
        /// <response code = "500">Internal server issue</response>
        [HttpPut]
        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data, Cities city, Categories category, Conditions condition)
        {
            try
            {
                // Update Listing entity from the service
                var result = await _listingService.Update(data,city, category, condition);

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
        //------------------------------------------------------------DELETE------------------------------------------------

        /// <summary>
        /// Deletes a listing 
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
