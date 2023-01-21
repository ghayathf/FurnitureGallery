using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureShop.Models;
using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Controllers
{
    public class FurnProductOrdersController : Controller
    {
        private readonly ModelContext _context;

        public FurnProductOrdersController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnProductOrders
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var modelContext = _context.FurnProductOrders.Include(f => f.Order).Include(f => f.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: FurnProductOrders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnProductOrder = await _context.FurnProductOrders
                .Include(f => f.Order)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnProductOrder == null)
            {
                return NotFound();
            }

            return View(furnProductOrder);
        }

        // GET: FurnProductOrders/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.FurnOrders, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.FurnProducts, "Id", "Id");
            return View();
        }

        // POST: FurnProductOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,ProductId,OrderId")] FurnProductOrder furnProductOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnProductOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.FurnOrders, "Id", "Id", furnProductOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.FurnProducts, "Id", "Id", furnProductOrder.ProductId);
            return View(furnProductOrder);
        }

        // GET: FurnProductOrders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnProductOrder = await _context.FurnProductOrders.FindAsync(id);
            if (furnProductOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.FurnOrders, "Id", "Id", furnProductOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.FurnProducts, "Id", "Id", furnProductOrder.ProductId);
            return View(furnProductOrder);
        }

        // POST: FurnProductOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Status,ProductId,OrderId")] FurnProductOrder furnProductOrder)
        {
            if (id != furnProductOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnProductOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnProductOrderExists(furnProductOrder.Id))
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
            ViewData["OrderId"] = new SelectList(_context.FurnOrders, "Id", "Id", furnProductOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.FurnProducts, "Id", "Id", furnProductOrder.ProductId);
            return View(furnProductOrder);
        }

        // GET: FurnProductOrders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnProductOrder = await _context.FurnProductOrders
                .Include(f => f.Order)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnProductOrder == null)
            {
                return NotFound();
            }

            return View(furnProductOrder);
        }

        // POST: FurnProductOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnProductOrder = await _context.FurnProductOrders.FindAsync(id);
            _context.FurnProductOrders.Remove(furnProductOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnProductOrderExists(decimal id)
        {
            return _context.FurnProductOrders.Any(e => e.Id == id);
        }
    }
}
