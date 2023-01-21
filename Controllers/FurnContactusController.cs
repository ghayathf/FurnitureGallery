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
    public class FurnContactusController : Controller
    {
        private readonly ModelContext _context;

        public FurnContactusController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnContactus
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View(await _context.FurnContactus.Where(p => p.Id == 1).ToListAsync());
        }

        // GET: FurnContactus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnContactu = await _context.FurnContactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnContactu == null)
            {
                return NotFound();
            }

            return View(furnContactu);
        }

        // GET: FurnContactus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FurnContactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Namee,Message")] FurnContactu furnContactu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnContactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnContactu);
        }

        // GET: FurnContactus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnContactu = await _context.FurnContactus.FindAsync(id);
            if (furnContactu == null)
            {
                return NotFound();
            }
            return View(furnContactu);
        }

        // POST: FurnContactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Email,Namee,Message")] FurnContactu furnContactu)
        {
            if (id != furnContactu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnContactu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnContactuExists(furnContactu.Id))
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
            return View(furnContactu);
        }

        // GET: FurnContactus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnContactu = await _context.FurnContactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnContactu == null)
            {
                return NotFound();
            }

            return View(furnContactu);
        }

        // POST: FurnContactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnContactu = await _context.FurnContactus.FindAsync(id);
            _context.FurnContactus.Remove(furnContactu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnContactuExists(decimal id)
        {
            return _context.FurnContactus.Any(e => e.Id == id);
        }
    }
}
