using MarketPlaceForYou.Models.ViewModels.Upload;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceForYou.Api.Controllers
{
    /// <summary>
    /// Upload one or more files
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="uploadService"></param>
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        /// <summary>
        /// API for uploading images
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<ActionResult<List<UploadResultVM>>> UploadImages()
        {
            //file validation
            var supportedTypes = new[] { ".png", ".jpeg", ".jpg", ".gif" };
            var uploadExtensions = Request.Form.Files.Select(i => System.IO.Path.GetExtension(i.FileName));
            var mismatchFound = uploadExtensions.Any(i => !supportedTypes.Contains(i));
            if (mismatchFound)
                return BadRequest("Uploaded file(s) are not a valid accepted type, please upload a png, jpeg, jpg or gif file");

            var results = await _uploadService.UploadImages(Request.Form.Files.ToList());

            return Ok(results);
        }
    }
}
