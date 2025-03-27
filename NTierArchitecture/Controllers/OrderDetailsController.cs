using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.CustomersDTO;
using static DataAccessLayer.DTOs.OrderDetailsDTO;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        public readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        [HttpPost("Create")]
        public IActionResult Create(OrderDetailsCreate orderDetailsCreateModel)
        {
            var response = _orderDetailsService.Create(orderDetailsCreateModel);

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
            var response = _orderDetailsService.Delete(id);

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
            var response = _orderDetailsService.ListAll();
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var response = _orderDetailsService.GetById(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByOrderNo")]
        public IActionResult GetByOrderNo(string orderNo)
        {
            var response = _orderDetailsService.GetByOrderNo(orderNo);

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
        public IActionResult Update(OrderDetailsUpdate orderDetailsUpdateModel)
        {
            var response = _orderDetailsService.Update(orderDetailsUpdateModel);
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
