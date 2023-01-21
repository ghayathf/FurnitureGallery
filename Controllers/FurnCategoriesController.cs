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
    public class FurnCategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FurnCategoriesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: FurnCategories
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View(await _context.FurnCategories.ToListAsync());
        }
        public async Task<IActionResult> Products(int Id)
        {
            var products = await _context.FurnProducts.Where(x => x.CategoryId == Id).ToListAsync();
            return View(products);
        }
        // GET: FurnCategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            if (id == null)
            {
                return NotFound();
            }

            var furnCategory = await _context.FurnCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnCategory == null)
            {
                return NotFound();
            }

            return View(furnCategory);
        }

        // GET: FurnCategories/Create
        public IActionResult Create()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View();
        }

        // POST: FurnCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Imagepath,ImageFile")] FurnCategory furnCategory)
        {
            if (ModelState.IsValid)
            {
                if (furnCategory.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + furnCategory.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await furnCategory.ImageFile.CopyToAsync(fileStream);
                    }
                    furnCategory.Imagepath = fileName;
                }
                _context.Add(furnCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnCategory);
        }

        // GET: FurnCategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnCategory = await _context.FurnCategories.FindAsync(id);
            if (furnCategory == null)
            {
                return NotFound();
            }
            return View(furnCategory);
        }

        // POST: FurnCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,Imagepath,ImageFile")] FurnCategory furnCategory)
        {
            if (id != furnCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (furnCategory.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + furnCategory.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await furnCategory.ImageFile.CopyToAsync(fileStream);
                        }
                        furnCategory.Imagepath = fileName;
                        _context.Update(furnCategory);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnCategoryExists(furnCategory.Id))
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
            return View(furnCategory);
        }

        // GET: FurnCategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnCategory = await _context.FurnCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnCategory == null)
            {
                return NotFound();
            }

            return View(furnCategory);
        }

        // POST: FurnCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnCategory = await _context.FurnCategories.FindAsync(id);
            _context.FurnCategories.Remove(furnCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnCategoryExists(decimal id)
        {
            return _context.FurnCategories.Any(e => e.Id == id);
        }
    }
}
