using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Bookstore_API.Services.OrdersFolder;

namespace Online_Bookstore_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("User/{UserName}")]
        public async Task<IActionResult> GetAllUserOrders(String UserName)
        {
            if (String.IsNullOrEmpty(UserName)) return BadRequest();
            var Orders = await _orderService.GetAllUserOrdersAsync(UserName);
            return Orders.Count == 0 ? NotFound("NO Orders Found..!") : Ok(Orders);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("Id/{OrderId}")]
        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            if (OrderId <=0  ) return BadRequest("Invalid Id.....!");
            var Order = await _orderService.GetOrderByIdAsync(OrderId);
            return Order ==null ? NotFound("NO Order Found..!") : Ok(Order);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var Orders = await _orderService.GetAllOrdersAsync();
            return Orders == null ? NotFound("NO Orders Found..!") : Ok(Orders);
        }

        [HttpPost("Create/{UserName}")]
        public async Task<IActionResult> CreateNewOrders(String UserName)
        {
            if (String.IsNullOrEmpty(UserName)) return BadRequest();
            var Order = await _orderService.CreateOrderAsync(UserName);
            return Order == null ? NotFound("Something Wrong..!") : Ok(Order);
        }
    }
}
