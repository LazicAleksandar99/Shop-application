using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.DTO.OrderDTO;
using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Interfaces.IServices;

namespace Shopping.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        //Customer
        public async Task<IActionResult> Create(CreateOrderDto newOrder)
        {
            var result = await _orderService.Create(newOrder);
            if (result == null)
                return BadRequest("Faild to create new order");

            return Ok(result);
        }

        [HttpGet("history/{id}")]
        //All? ja (Customer, Seller 100%) - nene ovo samo cust i sell jer za admina sve ide
        public async Task<IActionResult> History(int id)
        {
            var result = await _orderService.History(id);
            if (result == null)
                return BadRequest("Wrong Id");
            return Ok(result);
        }

        [HttpGet]
        //Administrator
        public async Task<IActionResult> AllOrder()
        {
            var result = await _orderService.AllOrders();
            return Ok(result);
        }

        //cancle Order
    }
}
