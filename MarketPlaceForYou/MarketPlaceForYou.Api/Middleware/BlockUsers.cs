using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Api.Helpers;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services;
using MarketPlaceForYou.Services.Services.Interfaces;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace MarketPlaceForYou.Api.Middleware
{
    /// <summary>
    /// Prevents a user who's attribute isBlocked is true from logging into the system
    /// </summary>
    public class BlockUsers
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor, anything before will be before the request coming in and after will be what will come back out.
        /// </summary>
        /// <param name="next"></param>
        public BlockUsers(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var blocked = userService.BlockUser;
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string errorMessage;

                switch (ex)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errorMessage = e.Message;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InsufficientStorage;
                        errorMessage = "Servers are down, please try again later";
                        break;
                }

                //var result = JsonSerializer.Serialize(new { message = errorMessage });
                //await response.WriteAsync(result);
            }
        }
    }
}
