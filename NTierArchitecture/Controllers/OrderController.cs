using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.OrderDetailsDTO;
using static DataAccessLayer.DTOs.OrdersDTO;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrderController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost("Create")]
        public IActionResult Create(OrdersCreate ordersCreateModel)
        {
            var response = _ordersService.Create(ordersCreateModel);

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
            var response = _ordersService.Delete(id);

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
            var response = _ordersService.ListAll();
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
            var response = _ordersService.GetById(id);

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
        public IActionResult Update(OrdersUpdate ordersUpdate)
        {
            var response = _ordersService.Update(ordersUpdate);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus(OrdersUpdateStatus ordersUpdateStatus)
        {
            var response = _ordersService.UpdateStatus(ordersUpdateStatus);
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
