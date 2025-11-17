using API.Data;
using API.DTO;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly DataContext _context;

    public CartController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CartDTO>> GetCart()
    {
        var cart = await GetOrCreate();
        return CartToDto(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddItemToCart(int productId, int quantity)
    {
        var cart = await GetOrCreate();

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        if (product == null)
        {
            return NotFound("The Product not in database");
        }

        cart.AddItem(product, quantity);
        var result = await _context.SaveChangesAsync() > 0;

        if (result)
            return CreatedAtAction(nameof(GetCart), cart);
        return BadRequest(new ProblemDetails { Title = "Product can not adding to cart" });
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteItemFromCart(int productId, int quantity)
    {
        var cart = await GetOrCreate();

        cart.DeleteItem(productId, quantity);
        
        var result = await _context.SaveChangesAsync() > 0;

        if (result)
            return Ok();
        return BadRequest(new ProblemDetails { Title = "Product can not removing from cart" });
    }
    private async Task<Cart> GetOrCreate()
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == Request.Cookies["customerId"]);

        if (cart == null)
        {
            var customerId = Guid.NewGuid().ToString();
            var cookiOptions = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Now.AddMonths(1)
            };

            Response.Cookies.Append("customerId", customerId, cookiOptions);
            cart = new Cart { CustomerId = customerId! };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

private CartDTO CartToDto(Cart cart)
    {
        return new CartDTO
        {
            Id = cart.CartId,
            CustomerId = cart.CustomerId,
            CartItems = cart.CartItems.Select(ci => new CartItemDTO
            {
                productId = ci.Product.Id,
                Name = ci.Product.Name,
                Price = ci.Product.Price,
                ImageUrl = ci.Product.ImageUrl,
                Quantity = ci.Quantity
            }).ToList()
        };
    }
}