using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.CustomersDTO;
using static DataAccessLayer.DTOs.ProductsDTO;

namespace PresentationLayer.Controllers
{
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
