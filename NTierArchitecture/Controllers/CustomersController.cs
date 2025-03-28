﻿using BusinessLayer.Interfaces;
using DataAccessLayer.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DataAccessLayer.DTOs.CustomersDTO;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public readonly ICustomersService _customersService;
        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpPost("Create")]
        public IActionResult Create(CustomerCreate customerCreateModel)
        {
            var response = _customersService.Create(customerCreateModel);

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
            var response = _customersService.Delete(id);

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
            var response = _customersService.ListAll();
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
            var response = _customersService.FindById(id);

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
        public IActionResult Update(CustomerUpdate customerUpdateModel)
        {
            var response = _customersService.Update(customerUpdateModel);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(int customerId, string password)
        {
            var response = _customersService.UpdatePassword(customerId, password);
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
