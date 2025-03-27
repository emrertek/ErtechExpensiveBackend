using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.OrdersDTO;
using static DataAccessLayer.DTOs.PaymentDTO;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Create")]
        public IActionResult Create(PaymentCreate paymentCreateModel)
        {
            var response = _paymentService.Create(paymentCreateModel);

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
            var response = _paymentService.Delete(id);

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
            var response = _paymentService.ListAll();
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByID")]
        public IActionResult GetByID(int id)
        {
            var response = _paymentService.GetByID(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

       
        [HttpGet("GetByMethod")]
        public IActionResult GetByMethod(string paymentMethod)
        {
            var response = _paymentService.GetByMethod(paymentMethod);

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
        public IActionResult Update(PaymentUpdate paymentUpdateModel)
        {
            var response = _paymentService.Update(paymentUpdateModel);
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
