using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureShop.Models;

namespace FurnitureShop.Controllers
{
    public class FurnOrdersController : Controller
    {
        private readonly ModelContext _context;

        public FurnOrdersController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnOrders
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.FurnOrders.Include(f => f.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: FurnOrders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnOrder = await _context.FurnOrders
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnOrder == null)
            {
                return NotFound();
            }

            return View(furnOrder);
        }

        // GET: FurnOrders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname");
            return View();
        }

        // POST: FurnOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Datee,UserId")] FurnOrder furnOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname", furnOrder.UserId);
            return View(furnOrder);
        }

        // GET: FurnOrders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnOrder = await _context.FurnOrders.FindAsync(id);
            if (furnOrder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname", furnOrder.UserId);
            return View(furnOrder);
        }

        // POST: FurnOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Datee,UserId")] FurnOrder furnOrder)
        {
            if (id != furnOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnOrderExists(furnOrder.Id))
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
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname", furnOrder.UserId);
            return View(furnOrder);
        }

        // GET: FurnOrders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnOrder = await _context.FurnOrders
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnOrder == null)
            {
                return NotFound();
            }

            return View(furnOrder);
        }

        // POST: FurnOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnOrder = await _context.FurnOrders.FindAsync(id);
            _context.FurnOrders.Remove(furnOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnOrderExists(decimal id)
        {
            return _context.FurnOrders.Any(e => e.Id == id);
        }
    }
}
