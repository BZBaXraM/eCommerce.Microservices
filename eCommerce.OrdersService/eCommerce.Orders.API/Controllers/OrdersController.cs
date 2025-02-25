namespace eCommerce.Orders.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrdersService ordersService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<OrderResponse?>> Get()
    {
        return await ordersService.GetOrdersAsync();
    }

    [HttpGet("search/orderId/{orderId}")]
    public async Task<OrderResponse?> GetOrderByOrderId(Guid orderId)
    {
        var filter = Builders<Order>.Filter.Eq(temp => temp.OrderId, orderId);

        return await ordersService.GetOrderByConditionAsync(filter);
    }
    
    [HttpGet("search/productId/{productId}")]
    public async Task<IEnumerable<OrderResponse?>> GetOrdersByProductId(Guid productId)
    {
        var filter = Builders<Order>.Filter.ElemMatch(temp => temp.OrderItems,
            Builders<OrderItem>.Filter.Eq(tempProduct => tempProduct.ProductId, productId)
        );

        return await ordersService.GetOrdersByConditionAsync(filter);
    }
    
    [HttpGet("/search/orderDate/{orderDate}")]
    public async Task<IEnumerable<OrderResponse?>> GetOrdersByOrderDate(DateTime orderDate)
    {
        var filter = Builders<Order>.Filter.Eq(temp => temp.OrderDate.ToString("yyyy-MM-dd"),
            orderDate.ToString("yyyy-MM-dd")
        );

        return await ordersService.GetOrdersByConditionAsync(filter);
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderAddRequest? orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        var orderResponse = await ordersService.AddOrderAsync(orderAddRequest);

        if (orderResponse == null)
        {
            return Problem("Error in adding order");
        }

        return CreatedAtAction(nameof(GetOrderByOrderId), new { orderId = orderResponse.OrderId }, orderResponse);
    }
    
    [HttpPut("{orderId}")]
    public async Task<IActionResult> Put(Guid orderId, OrderUpdateRequest? orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        if (orderId != orderUpdateRequest.OrderId)
        {
            return BadRequest("OrderID in the URL doesn't match with the OrderID in the Request body");
        }

        var orderResponse = await ordersService.UpdateOrderAsync(orderUpdateRequest);

        if (orderResponse == null)
        {
            return Problem("Error in updating order");
        }

        return Ok(orderResponse);
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest("Invalid order ID");
        }

        var isDeleted = await ordersService.DeleteOrderAsync(orderId);

        if (!isDeleted)
        {
            return Problem("Error in deleting order");
        }

        return Ok(isDeleted);
    }
}