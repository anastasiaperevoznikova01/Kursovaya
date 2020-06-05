using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T__Shop;

namespace T__Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly Tattoo_ShopContext _context;

        public OrdersController(Tattoo_ShopContext context)
        {
            _context = context;
        }

        // GET: Orders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tattoo_ShopContext = _context.Order.Include(o => o.Tattoo);

            return View(await tattoo_ShopContext.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var name = _context.Order.Where(c => (c.UserId == order.UserId) && (c.TattooId == order.TattooId)).FirstOrDefault();
            if (name == null) { 
            
                _context.Order.Add(order);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetColor", new { id = order.Id }, order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteColor(long id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
