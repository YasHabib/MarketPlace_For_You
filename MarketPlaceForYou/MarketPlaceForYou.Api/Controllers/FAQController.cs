using MarketPlaceForYou.Models.ViewModels.FAQ;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// Controller for FAQ
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;
        /// <summary>
        /// FAQ controller
        /// </summary>
        /// <param name="faqService"></param>
        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }
        /// <summary>
        /// Creating a FAQ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<ActionResult<FAQVM>> Create([FromBody] FAQaddVM data)
        {
            try
            {
                // Have the service create the new FAQ
                var result = await _faqService.Create(data);

                // Return a 200 response with the FAQVM
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
        /// Get an FAQ by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<FAQVM>> GetById([FromRoute] Guid id)
        {
            try
            {
                // Have the service create the new FAQ
                var result = await _faqService.GetById(id);

                // Return a 200 response with the FAQVM
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
        /// Get All FAQ (End user)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<FAQVM>>> GetAll()
        {
            try
            {
                // Have the service create the new FAQ
                var result = await _faqService.GetAll();

                // Return a 200 response with the FAQVM
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
        /// Endpoint for super admin to update an FAQ
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Authorize("Admin")]
        [HttpPut]
        public async Task<ActionResult<FAQVM>> Update([FromBody] FAQupdateVM data)
        {
            try
            {
                // Have the service create the new FAQ
                var result = await _faqService.Update(data);

                // Return a 200 response with the FAQVM
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
        /// Super admin deletes a FAQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _faqService.Delete(id);

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
