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
    public class FurnRolesController : Controller
    {
        private readonly ModelContext _context;

        public FurnRolesController(ModelContext context)
        {
            _context = context;
        }

        // GET: FurnRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.FurnRoles.ToListAsync());
        }

        // GET: FurnRoles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnRole = await _context.FurnRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnRole == null)
            {
                return NotFound();
            }

            return View(furnRole);
        }

        // GET: FurnRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FurnRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rolename")] FurnRole furnRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnRole);
        }

        // GET: FurnRoles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnRole = await _context.FurnRoles.FindAsync(id);
            if (furnRole == null)
            {
                return NotFound();
            }
            return View(furnRole);
        }

        // POST: FurnRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Rolename")] FurnRole furnRole)
        {
            if (id != furnRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnRoleExists(furnRole.Id))
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
            return View(furnRole);
        }

        // GET: FurnRoles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnRole = await _context.FurnRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnRole == null)
            {
                return NotFound();
            }

            return View(furnRole);
        }

        // POST: FurnRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnRole = await _context.FurnRoles.FindAsync(id);
            _context.FurnRoles.Remove(furnRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnRoleExists(decimal id)
        {
            return _context.FurnRoles.Any(e => e.Id == id);
        }
    }
}
