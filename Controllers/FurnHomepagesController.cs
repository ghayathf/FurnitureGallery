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
    public class FurnHomepagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FurnHomepagesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: FurnHomepages
        public async Task<IActionResult> Index()
        {
            
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View(await _context.FurnHomepages.ToListAsync());
        }

        // GET: FurnHomepages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnHomepage = await _context.FurnHomepages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnHomepage == null)
            {
                return NotFound();
            }

            return View(furnHomepage);
        }

        // GET: FurnHomepages/Create
        public IActionResult Create()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View();
        }

        // POST: FurnHomepages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImagePath,LogoPath,Paragraph,Email,Phone,Address,Text1,ImageFile,LogoFile")] FurnHomepage furnHomepage)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (ModelState.IsValid)
            {
                if (furnHomepage.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + furnHomepage.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await furnHomepage.ImageFile.CopyToAsync(fileStream);
                    }
                    furnHomepage.ImagePath = fileName;
                }
                if (furnHomepage.LogoFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + furnHomepage.LogoFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await furnHomepage.LogoFile.CopyToAsync(fileStream);
                    }
                    furnHomepage.LogoPath = fileName;
                }
                _context.Add(furnHomepage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnHomepage);
        }

        // GET: FurnHomepages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnHomepage = await _context.FurnHomepages.FindAsync(id);
            if (furnHomepage == null)
            {
                return NotFound();
            }
            return View(furnHomepage);
        }

        // POST: FurnHomepages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ImagePath,LogoPath,Paragraph,Email,Phone,Address,Text1,ImageFile,LogoFile")] FurnHomepage furnHomepage)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id != furnHomepage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (furnHomepage.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + furnHomepage.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await furnHomepage.ImageFile.CopyToAsync(fileStream);
                        }
                        furnHomepage.ImagePath = fileName;
                    }
                    if (furnHomepage.LogoFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + furnHomepage.LogoFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await furnHomepage.LogoFile.CopyToAsync(fileStream);
                        }
                        furnHomepage.LogoPath = fileName;
                    }
                    _context.Update(furnHomepage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnHomepageExists(furnHomepage.Id))
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
            return View(furnHomepage);
        }

        // GET: FurnHomepages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnHomepage = await _context.FurnHomepages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnHomepage == null)
            {
                return NotFound();
            }

            return View(furnHomepage);
        }

        // POST: FurnHomepages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var furnHomepage = await _context.FurnHomepages.FindAsync(id);
            _context.FurnHomepages.Remove(furnHomepage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnHomepageExists(decimal id)
        {
            return _context.FurnHomepages.Any(e => e.Id == id);
        }
    }
}
