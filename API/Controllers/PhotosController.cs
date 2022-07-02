using API.DTOs.KeyValuePairs;
using Core.Entites;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IBlobService _blobService;

        private readonly PhotoSettings _photoSettings;


        public PhotosController(IBlobService blobService, IOptionsSnapshot<PhotoSettings> options)
        {
            _blobService = blobService;
            _photoSettings = options.Value;
        }

        [HttpPost]
        public async Task<ActionResult<PhotoDto>> AddFile(IFormFile file)
        {
            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");


            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var res = await _blobService.UploadBlob(fileName, file, "images");

            if (res)
                return Ok(new PhotoDto { FileName = fileName });

            return BadRequest("Error While Uploading Photo");

        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFile([FromBody] PhotoDto photo)
        {
            var name = photo.FileName.Split('/').Last();
            await _blobService.DeleteBlob(name, "images");
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles()
        {
            var files = await _blobService.AllBlobs("images");

            return Ok(files);
        }

    }
}