using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static DataAccessLayer.DTOs.ImagesDTO;


namespace PresentationLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imagesService;
        private readonly IRedisService _redisService;
        private readonly IRedisCacheService _redisCacheService;

        public ImagesController(IImagesService imagesService, IRedisService redisService , IRedisCacheService redisCacheService)
        {
            _imagesService = imagesService;
            _redisService = redisService;
            _redisCacheService = redisCacheService;
        }

        // ImageCreate için (Base64 vs.)
        [HttpPost("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create(ImagesCreate imagesCreateModel)
        {
            var response = _imagesService.Create(imagesCreateModel);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // 🌟 IFormFile dosya yükleme
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAsync(IFormFile file,int productId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Yüklenecek bir dosya bulunamadı.");

            var response = _imagesService.UploadFile(file, productId);

            // Redis cache temizle
            await _redisService.RemoveAsync($"product_images_{productId}");

            return Ok(new { fileKey = response });
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id)
        {
            var response = _imagesService.Delete(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListAll")]
        [AllowAnonymous]
        public IActionResult ListAll()
        {
            var response = _imagesService.ListAll();
            return response.Success ? Ok(response) : BadRequest(response);
        }


        /* Guid ile resme ait bilgileri döndürür. */
        [HttpGet("FindByGuid")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult FindByGuid(Guid guid)
        {
            var response = _imagesService.FindByGuid(guid);
            return response.Success ? Ok(response) : BadRequest(response);
        }


        /* SADECE Base64 Formatı döndürür. */
        [HttpGet("GetImageBase64ByGuid")]
        [AllowAnonymous]
        public IActionResult GetImageBase64ByGuid(Guid guid)
        {
            var response = _imagesService.GetImageDataForFrontend(guid);
            return response.Success ? Ok(response) : NotFound(response.Message);
        }

        /* ProductId ile resmin Guid ve Base64 Formatını döndürür. */
        [HttpGet("GetImagesByProduct/{productId}")]
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImagesByProductAsync(int productId)
        {
            string cacheKey = $"product_images_{productId}";

            // 1. Redis'ten veriyi çek
            var cachedData = await _redisService.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var cachedResponse = JsonConvert.DeserializeObject<BusinessLayer.Common.Response.SuccessResponse<List<ProductImageDto>>>(cachedData);
                return Ok(cachedResponse);
            }

            // 2. Redis'te yoksa servis üzerinden getir
            var response = _imagesService.GetImagesByProductId(productId);

            if (!response.Success)
                return NotFound(response.Message);

            // 3. Redis'e kaydet
            var jsonData = JsonConvert.SerializeObject(response);
            await _redisService.SetAsync(cacheKey, jsonData);

            return Ok(response);
        }
        [HttpGet("RenderImage/{imgGuid}")]
        [AllowAnonymous]
        public IActionResult RenderImage(Guid imgGuid)
        {
            string cacheKey = $"render_image_{imgGuid}";
            string base64;

            // REDIS: Cache kontrol
            if (_redisCacheService.TryGet(cacheKey, out string cachedBase64))
            {
                base64 = cachedBase64;
            }
            else
            {
                // DB'den çek
                var result = _imagesService.FindByGuid(imgGuid);

                if (!result.Success || result.Data == null)
                    return NotFound("Image not found");

                base64 = result.Data.Base64ImagesFormat;

                if (string.IsNullOrWhiteSpace(base64))
                    return BadRequest("Base64 data is empty");

                // REDIS: Cache'e ekle
                _redisCacheService.Set(cacheKey, base64, TimeSpan.FromMinutes(60)); // süresi ihtiyaca göre
            }

            // Prefix varsa temizle
            if (base64.Contains(","))
                base64 = base64.Split(',')[1];

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64);
            }
            catch
            {
                return BadRequest("Invalid base64 string.");
            }

            // Content type belirle
            string contentType = "image/jpeg";
            if (base64.StartsWith("iVBOR")) contentType = "image/png";

            return File(imageBytes, contentType);
        }

    }
}
