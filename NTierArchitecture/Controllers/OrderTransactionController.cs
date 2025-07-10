using BusinessLayer.Interfaces;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class OrderTransactionController : ControllerBase
{
    private readonly IOrderTransactionService _orderTransactionService;

    public OrderTransactionController(IOrderTransactionService orderTransactionService)
    {
        _orderTransactionService = orderTransactionService;
    }

    [HttpPost("Create")]
    [AllowAnonymous]
    public IActionResult Create([FromBody] OrderTransactionDTO.Create model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                          .Select(e => e.ErrorMessage)
                          .ToList();

            return BadRequest(new
            {
                message = "Validation errors occurred",
                errors = errors
            });
        }

        var response = _orderTransactionService.CreateCompleteOrder(
            model.Order,
            model.OrderDetails,
            model.Payment,
            model.Address
        );

        if (response.Success)
        {
            return Ok(response);
        }

        return BadRequest(new
        {
            message = response.Message
        });
    }
}
