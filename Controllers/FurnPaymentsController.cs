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
    public class FurnPaymentsController : Controller
    {
        private readonly ModelContext _context;

        public FurnPaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnPayments
        public async Task<IActionResult> Index(DateTime firstdate, DateTime enddate)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            //var modelContext = _context.FurnPayments.Include(f => f.User);
            //return View(await modelContext.ToListAsync());
            var q = _context.FurnPayments
            .Where(n => n.Paydate >= firstdate)
            .Where(n => n.Paydate <= enddate);
            return View(q);
        }

        // GET: FurnPayments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnPayment = await _context.FurnPayments
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnPayment == null)
            {
                return NotFound();
            }

            return View(furnPayment);
        }

        // GET: FurnPayments/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id");
            return View();
        }

        // POST: FurnPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Paydate,UserId")] FurnPayment furnPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnPayment.UserId);
            return View(furnPayment);
        }

        // GET: FurnPayments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnPayment = await _context.FurnPayments.FindAsync(id);
            if (furnPayment == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnPayment.UserId);
            return View(furnPayment);
        }

        // POST: FurnPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Amount,Paydate,UserId")] FurnPayment furnPayment)
        {
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id != furnPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnPaymentExists(furnPayment.Id))
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
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnPayment.UserId);
            return View(furnPayment);
        }

        // GET: FurnPayments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnPayment = await _context.FurnPayments
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnPayment == null)
            {
                return NotFound();
            }

            return View(furnPayment);
        }

        // POST: FurnPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnPayment = await _context.FurnPayments.FindAsync(id);
            _context.FurnPayments.Remove(furnPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnPaymentExists(decimal id)
        {
            return _context.FurnPayments.Any(e => e.Id == id);
        }
    }
}
