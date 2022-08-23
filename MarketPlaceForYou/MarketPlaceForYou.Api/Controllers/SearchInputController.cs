using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Models.ViewModels.SearchInput;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{/// <summary>
/// Search input controller
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchInputController : ControllerBase
    {
        private readonly ISearchInputService _searchInputService;
        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="searchInputService"></param>
        public SearchInputController(ISearchInputService searchInputService)
        {
            _searchInputService = searchInputService;
        }
        /// <summary>
        /// Saving the search string
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<SearchInputVM>> SaveSearch([FromQuery] SearchInputAddVM data, string? searchString=null)
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");
                // Have the service to save the search input
                var result = await _searchInputService.SaveSearch(data, userId, searchString);

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
        /// Retrieving search history and display to user (if needed)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<SearchInputVM>>> Get3()
        {
            try
            {
                var userId = User.GetId();
                if (userId == null)
                    return BadRequest("Invalid Request");
                // Have the service to save the search input
                var result = await _searchInputService.Get3(userId);

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
