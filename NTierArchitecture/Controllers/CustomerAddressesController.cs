using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.CustomerAddressesDTO;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly ICustomerAddressesService _addressService;

        public CustomerAddressesController(ICustomerAddressesService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("Create")]
        public IActionResult Create(AddressCreate model)
        {
            var response = _addressService.Create(model);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public IActionResult Update(AddressUpdate model)
        {
            var response = _addressService.Update(model);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var response = _addressService.Delete(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            var response = _addressService.ListAll();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByCustomer")]
        public IActionResult GetByCustomer(int customerId)
        {
            var response = _addressService.FindByCustomerId(customerId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
