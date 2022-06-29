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

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

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

        [HttpGet("all/{category}")]
        public async Task<ActionResult<List<ListingVM>>> GetAllByCategory(string category)
        {
            try
            {
                // Get the listing entities from the service
                var results = await _listingService.GetAllByCity(category);

                // Return a 200 response with the ListingVMs
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

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
