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
    public class FurnLoginsController : Controller
    {
        private readonly ModelContext _context;

        public FurnLoginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnLogins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.FurnLogins.Include(f => f.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: FurnLogins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnLogin = await _context.FurnLogins
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnLogin == null)
            {
                return NotFound();
            }

            return View(furnLogin);
        }

        // GET: FurnLogins/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id");
            return View();
        }

        // POST: FurnLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Passwordd,UserId")] FurnLogin furnLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnLogin.UserId);
            return View(furnLogin);
        }

        // GET: FurnLogins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnLogin = await _context.FurnLogins.FindAsync(id);
            if (furnLogin == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnLogin.UserId);
            return View(furnLogin);
        }

        // POST: FurnLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Username,Passwordd,UserId")] FurnLogin furnLogin)
        {
            if (id != furnLogin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnLoginExists(furnLogin.Id))
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
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnLogin.UserId);
            return View(furnLogin);
        }

        // GET: FurnLogins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnLogin = await _context.FurnLogins
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnLogin == null)
            {
                return NotFound();
            }

            return View(furnLogin);
        }

        // POST: FurnLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnLogin = await _context.FurnLogins.FindAsync(id);
            _context.FurnLogins.Remove(furnLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnLoginExists(decimal id)
        {
            return _context.FurnLogins.Any(e => e.Id == id);
        }
    }
}
