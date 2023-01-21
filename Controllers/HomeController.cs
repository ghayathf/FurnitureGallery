using FurnitureShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var page = _context.FurnHomepages.Where(p => p.Id == 1).First();
            HttpContext.Session.SetString("Address", page.Address);
            HttpContext.Session.SetString("Text1", page.Text1);
            HttpContext.Session.SetString("Email", page.Email);
            HttpContext.Session.SetString("Phone", page.Phone);
            HttpContext.Session.SetString("Paragraph", page.Paragraph);
            HttpContext.Session.SetString("Image", page.ImagePath);
            HttpContext.Session.SetString("Logo", page.LogoPath);


            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.Paragraph = HttpContext.Session.GetString("Paragraph");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Text1 = HttpContext.Session.GetString("Text1");
            ViewBag.Phone = HttpContext.Session.GetString("Phone");
            ViewBag.Image = HttpContext.Session.GetString("Image");
            ViewBag.Logo = HttpContext.Session.GetString("Logo");
            var catt = await _context.FurnCategories.ToListAsync();
            var tes = await _context.FurnTestimonials.Include(x=>x.User).Where(x => x.Status == "1").ToListAsync();
            var prods = await _context.FurnProducts.ToListAsync();
            var model = Tuple.Create<IEnumerable<FurnCategory>,IEnumerable<FurnTestimonial>,IEnumerable<FurnProduct>>(catt, tes,prods);
            return View(model);

        }

        public async Task<IActionResult> Products(int Id)
        {
            var page = _context.FurnHomepages.Where(p => p.Id == 1).First();
            HttpContext.Session.SetString("Address", page.Address);
            HttpContext.Session.SetString("Text1", page.Text1);
            HttpContext.Session.SetString("Email", page.Email);
            HttpContext.Session.SetString("Phone", page.Phone);
            HttpContext.Session.SetString("Paragraph", page.Paragraph);
            HttpContext.Session.SetString("Image", page.ImagePath);
            HttpContext.Session.SetString("Logo", page.LogoPath);


            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.Paragraph = HttpContext.Session.GetString("Paragraph");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Text1 = HttpContext.Session.GetString("Text1");
            ViewBag.Phone = HttpContext.Session.GetString("Phone");
            ViewBag.Image = HttpContext.Session.GetString("Image");
            ViewBag.Logo = HttpContext.Session.GetString("Logo");
            var products = await _context.FurnProducts.Where(x => x.CategoryId == Id).ToListAsync();
            return View(products);
        }
        public IActionResult About()
        {
            var page = _context.FurnAbouts.Where(p => p.Id == 1).First();
            HttpContext.Session.SetString("AboutImage", page.ImagePath);
            HttpContext.Session.SetString("AboutParagraph1", page.Paragraph1);
            HttpContext.Session.SetString("AboutParagraph2", page.Paragraph2);
            HttpContext.Session.SetString("AboutParagraph3", page.Paragraph3);
            ViewBag.AboutParagraph1 = HttpContext.Session.GetString("AboutParagraph1");
            ViewBag.AboutParagraph2 = HttpContext.Session.GetString("AboutParagraph2");
            ViewBag.AboutParagraph3 = HttpContext.Session.GetString("AboutParagraph3");
            ViewBag.AboutImage = HttpContext.Session.GetString("AboutImage");



            ViewBag.Paragraph = HttpContext.Session.GetString("Paragraph");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Text1 = HttpContext.Session.GetString("Text1");
            ViewBag.Phone = HttpContext.Session.GetString("Phone");
            ViewBag.Image = HttpContext.Session.GetString("Image");
            ViewBag.Logo = HttpContext.Session.GetString("Logo");
            return View();
        }
        
        
        public IActionResult Contact()
        {
            var page = _context.FurnContactus.Where(p => p.Id == 1).First();
            HttpContext.Session.SetString("EmailContact", page.Email);
            HttpContext.Session.SetString("NameContact", page.Namee);
            HttpContext.Session.SetString("MessageContact", page.Message);
            ViewBag.EmailContact = HttpContext.Session.GetString("EmailContact");
            ViewBag.NameContact = HttpContext.Session.GetString("NameContact");
            ViewBag.MessageContact = HttpContext.Session.GetString("MessageContact");



            ViewBag.Paragraph = HttpContext.Session.GetString("Paragraph");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Text1 = HttpContext.Session.GetString("Text1");
            ViewBag.Phone = HttpContext.Session.GetString("Phone");
            ViewBag.Image = HttpContext.Session.GetString("Image");
            ViewBag.Logo = HttpContext.Session.GetString("Logo");

            ViewBag.usernamee = HttpContext.Session.GetString("usernamee");
            ViewBag.id = HttpContext.Session.GetInt32("id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Id,Email,Namee,Message")] FurnContactu furnContactu)
        {
            var page = _context.FurnContactus.Where(p => p.Id == 1).First();
            HttpContext.Session.SetString("EmailContact", page.Email);
            HttpContext.Session.SetString("NameContact", page.Namee);
            HttpContext.Session.SetString("MessageContact", page.Message);
            ViewBag.EmailContact = HttpContext.Session.GetString("EmailContact");
            ViewBag.NameContact = HttpContext.Session.GetString("NameContact");
            ViewBag.MessageContact = HttpContext.Session.GetString("MessageContact");



            ViewBag.Paragraph = HttpContext.Session.GetString("Paragraph");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Text1 = HttpContext.Session.GetString("Text1");
            ViewBag.Phone = HttpContext.Session.GetString("Phone");
            ViewBag.Image = HttpContext.Session.GetString("Image");
            ViewBag.Logo = HttpContext.Session.GetString("Logo");

            ViewBag.usernamee = HttpContext.Session.GetString("usernamee");
            ViewBag.id = HttpContext.Session.GetInt32("id");

            if (ModelState.IsValid)
            {
                _context.Add(furnContactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Contact));
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
