using MarketPlaceForYou.Models.ViewModels.APAnalytics;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class APDashboardController : ControllerBase
    {
        private readonly IAPDashboardService _aPDashboardService;

        /// <summary>
        /// Controller for Admin panel dashboard functionalities
        /// </summary>
        /// <param name="aPDashboardService"></param>
        public APDashboardController(IAPDashboardService aPDashboardService)
        {
            _aPDashboardService = aPDashboardService;
        }

        /// <summary>
        /// Returns total number of users (unblocked) as integer
        /// </summary>
        /// <returns></returns>
        [HttpGet("totalUsers")]
        public async Task<ActionResult<int>> TotalUsers()
        {
            try
            {
                return await _aPDashboardService.TotalUsers();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Returns total number of non-deleted listings as integer
        /// </summary>
        /// <returns></returns>
        [HttpGet("totalListings")]
        public async Task<ActionResult<int>> TotalListings()
        {
            try
            {
                return await _aPDashboardService.TotalListings();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// returns the total number of users has joined today
        /// </summary>
        /// <returns></returns>
        [HttpGet("newUsers")]
        public async Task<ActionResult<int>> NewUsers()
        {
            try
            {
                return await _aPDashboardService.NewUsers();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Returns the average sales per day calculated over the last year
        /// </summary>
        /// <returns></returns>
        [HttpGet("avgSalesPerDay")]
        public async Task<ActionResult<decimal>> AvgSalesPerDay()
        {
            try
            {
                return await _aPDashboardService.PerDayAvgSalesInYear();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Grabs all the sales per day over last year + the dates + % increase
        /// </summary>
        /// <returns></returns>
        [HttpGet("salesPerDay")]
        public async Task<ActionResult<SalesPerDayVM>> SalesPerDay([FromBody]DateTime start, DateTime end)
        {
            try
            {
                return await _aPDashboardService.SalesPerDayOverAMonth(start,end);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
