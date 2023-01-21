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
    public class FurnAboutsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FurnAboutsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

      //  GET: FurnAbouts
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View(await _context.FurnAbouts.ToListAsync());
        }

      //  GET: FurnAbouts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnAbout = await _context.FurnAbouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnAbout == null)
            {
                return NotFound();
            }

            return View(furnAbout);
        }

      //  GET: FurnAbouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FurnAbouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImagePath,ImageFile,Paragraph1,Paragraph2,Paragraph3")] FurnAbout furnAbout)
        {
            if (ModelState.IsValid)
            {
                if (furnAbout.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + "_" + furnAbout.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await furnAbout.ImageFile.CopyToAsync(fileStream);
                    }
                    furnAbout.ImagePath = fileName;
                }
                _context.Add(furnAbout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnAbout);
        }

      //  GET: FurnAbouts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (id == null)
            {
                return NotFound();
            }

            var furnAbout = await _context.FurnAbouts.FindAsync(id);
            if (furnAbout == null)
            {
                return NotFound();
            }
            return View(furnAbout);
        }

       // POST: FurnAbouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ImagePath,ImageFile,Paragraph1,Paragraph2,Paragraph3")] FurnAbout furnAbout)
        {
            if (id != furnAbout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (furnAbout.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;

                        string fileName = Guid.NewGuid().ToString() + "_" + furnAbout.ImageFile.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await furnAbout.ImageFile.CopyToAsync(fileStream);
                        }
                        furnAbout.ImagePath = fileName;
                        _context.Update(furnAbout);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnAboutExists(furnAbout.Id))
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
            return View(furnAbout);
        }

       // GET: FurnAbouts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnAbout = await _context.FurnAbouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnAbout == null)
            {
                return NotFound();
            }

            return View(furnAbout);
        }

       // POST: FurnAbouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furnAbout = await _context.FurnAbouts.FindAsync(id);
            _context.FurnAbouts.Remove(furnAbout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnAboutExists(decimal id)
        {
            return _context.FurnAbouts.Any(e => e.Id == id);
            }
        }
    }
