using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.CustomersDTO;
using static DataAccessLayer.DTOs.ProductsDTO;

namespace PresentationLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create(ProductsCreate productsCreateModel)
        {
            var response = _productsService.Create(productsCreateModel);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id)
        {
            var response = _productsService.Delete(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("ListAll")]
        //[Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public IActionResult List()
        {
            var response = _productsService.ListAll();
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("FindById")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult FindById(int id)
        {
            var response = _productsService.FindById(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Update(ProductsUpdate productsUpdateModel)
        {
            var response = _productsService.Update(productsUpdateModel);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
