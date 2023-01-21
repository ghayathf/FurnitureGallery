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
    public class FurnBanksController : Controller
    {
        private readonly ModelContext _context;

        public FurnBanksController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnBanks
        public async Task<IActionResult> Index()
        {

            return View(await _context.FurnBanks.ToListAsync());
        }

        // GET: FurnBanks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnBank = await _context.FurnBanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnBank == null)
            {
                return NotFound();
            }

            return View(furnBank);
        }

        // GET: FurnBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FurnBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardNumber,Cvv,Amount")] FurnBank furnBank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnBank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnBank);
        }

        // GET: FurnBanks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnBank = await _context.FurnBanks.FindAsync(id);
            if (furnBank == null)
            {
                return NotFound();
            }
            return View(furnBank);
        }

        // POST: FurnBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CardNumber,Cvv,Amount")] FurnBank furnBank)
        {
            if (id != furnBank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnBankExists(furnBank.Id))
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
            return View(furnBank);
        }

        // GET: FurnBanks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnBank = await _context.FurnBanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnBank == null)
            {
                return NotFound();
            }

            return View(furnBank);
        }

        // POST: FurnBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnBank = await _context.FurnBanks.FindAsync(id);
            _context.FurnBanks.Remove(furnBank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnBankExists(decimal id)
        {
            return _context.FurnBanks.Any(e => e.Id == id);
        }
    }
}
