using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MKPFYController : ControllerBase
    {
        private readonly IUserService _userService;

        public MKPFYController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("update")]

        public async Task<ActionResult<UsersEntity>> Update([FromBody] UserUpdateVM data)
        {
            try
            {
                // Update user entity from the service
                var result = await _userService.Update(data);

                // Return a 200 response
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
