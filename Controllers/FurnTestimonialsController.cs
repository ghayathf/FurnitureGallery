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
    public class FurnTestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public FurnTestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnTestimonials
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var modelContext = _context.FurnTestimonials.Include(f => f.User);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> addTesti(decimal idd)
        {
            var tt = await _context.FurnTestimonials.FindAsync(idd);
            tt.Status = "1";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Accept(decimal id)
        {
            var testimonial =await _context.FurnTestimonials.FindAsync(id);
            testimonial.Status = "1";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(decimal id)
        {
            var testimonial =await _context.FurnTestimonials.FindAsync(id);
            testimonial.Status = "0";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: FurnTestimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnTestimonial = await _context.FurnTestimonials
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnTestimonial == null)
            {
                return NotFound();
            }

            return View(furnTestimonial);
        }

        // GET: FurnTestimonials/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname");
            return View();
        }

        // POST: FurnTestimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,UserId")] FurnTestimonial furnTestimonial)
        {
            if (ModelState.IsValid)
            {
                furnTestimonial.Status = "0";
                _context.Add(furnTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname", furnTestimonial.UserId);
            return View(furnTestimonial);
        }

        // GET: FurnTestimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnTestimonial = await _context.FurnTestimonials.FindAsync(id);
            if (furnTestimonial == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname", furnTestimonial.UserId);
            return View(furnTestimonial);
        }

        // POST: FurnTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Message,UserId")] FurnTestimonial furnTestimonial)
        {
            if (id != furnTestimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnTestimonialExists(furnTestimonial.Id))
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
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Fullname", furnTestimonial.UserId);
            return View(furnTestimonial);
        }

        // GET: FurnTestimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnTestimonial = await _context.FurnTestimonials
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnTestimonial == null)
            {
                return NotFound();
            }

            return View(furnTestimonial);
        }

        // POST: FurnTestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnTestimonial = await _context.FurnTestimonials.FindAsync(id);
            _context.FurnTestimonials.Remove(furnTestimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnTestimonialExists(decimal id)
        {
            return _context.FurnTestimonials.Any(e => e.Id == id);
        }
    }
}
