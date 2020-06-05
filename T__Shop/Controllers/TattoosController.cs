using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T__Shop;

namespace T__Shop.Controllers
{
    public class TattoosController : Controller
    {
        private readonly Tattoo_ShopContext _context;

        public TattoosController(Tattoo_ShopContext context)
        {
            _context = context;
        }

        // GET: Tattoos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tattoo.ToListAsync());
        }

        // GET: Tattoos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tattoo == null)
            {
                return NotFound();
            }

            return View(tattoo);
        }

        // GET: Tattoos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tattoos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> Image,[Bind("Id,Name,Price")] Tattoo tattoo)
        {
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        tattoo.Image = stream.ToArray();
                    }
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(tattoo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tattoo);
        }

        // GET: Tattoos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoo.FindAsync(id);
            if (tattoo == null)
            {
                return NotFound();
            }
            return View(tattoo);
        }

        // POST: Tattoos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Price,Image")] Tattoo tattoo)
        {
            if (id != tattoo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tattoo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TattooExists(tattoo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tattoo);
        }

        // GET: Tattoos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tattoo = await _context.Tattoo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tattoo == null)
            {
                return NotFound();
            }

            return View(tattoo);
        }

        // POST: Tattoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tattoo = await _context.Tattoo.FindAsync(id);
            _context.Tattoo.Remove(tattoo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TattooExists(long id)
        {
            return _context.Tattoo.Any(e => e.Id == id);
        }
    }
}
