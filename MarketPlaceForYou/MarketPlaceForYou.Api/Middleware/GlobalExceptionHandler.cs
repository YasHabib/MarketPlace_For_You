namespace MarketPlaceForYou.Api.Middleware
{
    /// <summary>
    /// Global Exception handler
    /// </summary>
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch
            {

            }
        }
    }
}
