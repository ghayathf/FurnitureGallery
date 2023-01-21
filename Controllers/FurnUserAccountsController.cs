using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureShop.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FurnitureShop.Controllers
{
    public class FurnUserAccountsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FurnUserAccountsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: FurnUserAccounts
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var modelContext = _context.FurnUserAccounts.Include(f => f.Role).Where(x => x.RoleId == 2);
            return View(await modelContext.ToListAsync());
        }

        // GET: FurnUserAccounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnUserAccount = await _context.FurnUserAccounts
                .Include(f => f.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnUserAccount == null)
            {
                return NotFound();
            }

            return View(furnUserAccount);
        }

        // GET: FurnUserAccounts/Create
        public IActionResult Create()
        {

            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename");
            return View();
        }

        // POST: FurnUserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,Phone,ImagePath,Email,RoleId,ImageFile")] FurnUserAccount furnUserAccount)
        {
            if (ModelState.IsValid)
            {
                if (furnUserAccount.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + furnUserAccount.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await furnUserAccount.ImageFile.CopyToAsync(fileStream);
                    }
                    furnUserAccount.ImagePath = fileName;
                }
                _context.Add(furnUserAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }

        // GET: FurnUserAccounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnUserAccount = await _context.FurnUserAccounts.FindAsync(id);
            if (furnUserAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }

        // POST: FurnUserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fullname,Phone,ImagePath,Email,RoleId,ImageFile")] FurnUserAccount furnUserAccount)
        {
            if (id != furnUserAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (furnUserAccount.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + furnUserAccount.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await furnUserAccount.ImageFile.CopyToAsync(fileStream);
                        }
                        furnUserAccount.ImagePath = fileName;

                    }
                    _context.Update(furnUserAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnUserAccountExists(furnUserAccount.Id))
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
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }

        // GET: FurnUserAccounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnUserAccount = await _context.FurnUserAccounts
                .Include(f => f.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnUserAccount == null)
            {
                return NotFound();
            }

            return View(furnUserAccount);
        }

        // POST: FurnUserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnUserAccount = await _context.FurnUserAccounts.FindAsync(id);
            _context.FurnUserAccounts.Remove(furnUserAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnUserAccountExists(decimal id)
        {
            return _context.FurnUserAccounts.Any(e => e.Id == id);
        }
    }
}
