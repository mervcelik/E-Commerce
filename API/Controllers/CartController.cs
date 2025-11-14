using API.Data;
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
    public async Task<ActionResult<Cart>> GetCart()
    {
        var cart = await GetOrCreate();
        return cart;
    }

    [HttpPost]
    public async Task<ActionResult> AddItemToCart(int productId, int quantity)
    {
        var cart = await GetOrCreate();

        var product = await _context.Products.FirstOrDefaultAsync(x=>x.Id==productId);
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
}