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
    public class FurnProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FurnProductsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: FurnProducts
        public IActionResult Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.total = _context.FurnProducts.Sum(p => p.Price);
            var modelContext = _context.FurnProducts.OrderByDescending(p => p.Valuee).Include(f => f.Category);
            return View(modelContext);
        }
        [HttpPost]
        public IActionResult Index(int id)
        {
            var selectedCategory = _context.FurnCategories.Where(c => c.Id == id).FirstOrDefault();

            // Get the list of products that belong to the selected category.
            var products = _context.FurnProducts.Where(x => x.Category.Id == selectedCategory.Id);

            // Return the list of products to the view.
            return View(products.ToListAsync());
        }

        // GET: FurnProducts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnProduct = await _context.FurnProducts
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnProduct == null)
            {
                return NotFound();
            }

            return View(furnProduct);
        }

        // GET: FurnProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.FurnCategories, "Id", "CategoryName");
            return View();
        }

        // POST: FurnProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Namee,Price,Valuee,ImagePath,ImageFile,CategoryId")] FurnProduct furnProduct)
        {
            if (ModelState.IsValid)
            {
                if (furnProduct.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + furnProduct.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await furnProduct.ImageFile.CopyToAsync(fileStream);
                    }
                    furnProduct.ImagePath = fileName;
                }
                _context.Add(furnProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.FurnCategories, "Id", "CategoryName", furnProduct.CategoryId);
            return View(furnProduct);
        }

        // GET: FurnProducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnProduct = await _context.FurnProducts.FindAsync(id);
            if (furnProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.FurnCategories, "Id", "CategoryName", furnProduct.CategoryId);
            return View(furnProduct);
        }

        // POST: FurnProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Namee,Price,Valuee,ImagePath,ImageFile,CategoryId")] FurnProduct furnProduct)
        {
            if (id != furnProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (furnProduct.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + furnProduct.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await furnProduct.ImageFile.CopyToAsync(fileStream);
                        }
                        furnProduct.ImagePath = fileName;

                    }
                    _context.Update(furnProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnProductExists(furnProduct.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.FurnCategories, "Id", "CategoryName", furnProduct.CategoryId);
            return View(furnProduct);
        }

        // GET: FurnProducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnProduct = await _context.FurnProducts
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnProduct == null)
            {
                return NotFound();
            }

            return View(furnProduct);
        }

        // POST: FurnProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnProduct = await _context.FurnProducts.FindAsync(id);
            _context.FurnProducts.Remove(furnProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnProductExists(decimal id)
        {
            return _context.FurnProducts.Any(e => e.Id == id);
        }
    }
}
