using FurnitureShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using SmtpClient = System.Net.Mail.SmtpClient;
using Aspose.Pdf;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;
using System.Reflection.PortableExecutable;
using static iTextSharp.text.pdf.AcroFields;

namespace FurnitureShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomerController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.id = HttpContext.Session.GetInt32("id");
            var categories = await _context.FurnCategories.OrderBy(p=>p.CategoryName).ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> productsForCategory(int id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var pros = await _context.FurnProducts.Where(x => x.CategoryId == id).Include(x => x.Category).ToListAsync();
            return View(pros);
        }
        public async Task<IActionResult> Products(string Prodd, string SearchString)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            if (_context.FurnProducts == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.FurnProducts
                                            orderby m.Namee
                                            select m.Namee;
            var movies = from m in _context.FurnProducts.Include(p=>p.Category)
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Namee!.Contains(SearchString)).Include(p=>p.Category);
            }

            if (!string.IsNullOrEmpty(Prodd))
            {
                movies = movies.Where(x => x.Namee == Prodd).Include(p=>p.Category);
            }

            var prodsss = new prodSearch
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                products = await movies.OrderByDescending(p => p.Price).ToListAsync()
            };
            
            return View(prodsss);
            //var modelContext = _context.FurnProducts.Include(f => f.Category);
            //return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> EditProfile()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.username = HttpContext.Session.GetString("username");
            decimal num = (decimal)HttpContext.Session.GetInt32("id");

            var furnUserAccount = await _context.FurnUserAccounts.FindAsync(num);
            if (furnUserAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(decimal id, [Bind("Id,Fullname,Phone,Email,RoleId,ImageFile")] FurnUserAccount furnUserAccount, string pass)
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
                    furnUserAccount.RoleId = 2;
                    _context.Update(furnUserAccount);
                    await _context.SaveChangesAsync();
                    var log = _context.FurnLogins.Where(x => x.UserId == furnUserAccount.Id).FirstOrDefault();
                    log.Username = furnUserAccount.Email;
                    log.Passwordd = pass;
                    log.UserId = furnUserAccount.Id;
                    _context.Update(log);
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
                return RedirectToAction("ViewProfile", "Customer");
            }
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }
        private bool FurnUserAccountExists(decimal id)
        {
            return _context.FurnUserAccounts.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ViewProfile()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.username = HttpContext.Session.GetString("username");
            decimal num = (decimal)HttpContext.Session.GetInt32("id");

            var furnUserAccount = await _context.FurnUserAccounts.FindAsync(num);
            if (furnUserAccount == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }

        public IActionResult Testimonial()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Testimonial([Bind("Id,Message,UserId")] FurnTestimonial furnTestimonial)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            if (ModelState.IsValid)
            {
                decimal num = (decimal)HttpContext.Session.GetInt32("id");
                furnTestimonial.Status = "0";
                furnTestimonial.UserId = num;
                _context.Add(furnTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.FurnUserAccounts, "Id", "Id", furnTestimonial.UserId);
            return View(furnTestimonial);
        }

        [HttpPost]
        public string Products(string SearchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + SearchString;
        }
        public IActionResult Join()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var user = _context.FurnUserAccounts.ToList();
            var product = _context.FurnProducts.ToList();
            var order = _context.FurnOrders.ToList();
            var productOrder = _context.FurnProductOrders.ToList();
            var payment = _context.FurnPayments.ToList();

            var model = from p in payment
                        join u in user on p.UserId equals u.Id
                        join o in order on u.Id equals o.UserId
                        join po in productOrder on o.Id equals po.OrderId
                        join pr in product on po.ProductId equals pr.Id
                        select new CartTable
                        {
                            furnProduct = pr,
                            furnProductOrder = po,
                            furnUserAccount = u,
                            furnOrder = o,
                            furnPayment = p
                        };
            return View(model);
        }
        public IActionResult AddToFavourate(decimal id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");


            decimal num = (decimal)HttpContext.Session.GetInt32("id");
            FurnFavourate furnFavourate = new FurnFavourate();
            furnFavourate.ProductId = id;
            furnFavourate.UserId = num;
            _context.Add(furnFavourate);
            _context.SaveChanges();
            return RedirectToAction("MyFavourate", "Customer");
        }
        public IActionResult MyFavourate()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            decimal num = (decimal)HttpContext.Session.GetInt32("id");
            var favs = _context.FurnFavourates.Where(p => p.UserId == num).Include(p => p.Product).Include(p => p.Product.Category).Include(p => p.User);
            favs.OrderByDescending(p => p.Product.Price);
            return View(favs);
        }
        public async Task<IActionResult> RemoveFavourate(decimal id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");

            var favs = await _context.FurnFavourates.FindAsync(id);
            _context.FurnFavourates.Remove(favs);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyFavourate");
        }
        public IActionResult shopping(decimal id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            decimal num = (decimal)HttpContext.Session.GetInt32("id");
            FurnOrder furnOrder = new FurnOrder();
            furnOrder.UserId = num;
            furnOrder.Datee = DateTime.Now;

            _context.Add(furnOrder);
            _context.SaveChanges();



            FurnProductOrder furnProductOrder = new FurnProductOrder();
            furnProductOrder.OrderId = furnOrder.Id;
            furnProductOrder.ProductId = id;
            furnProductOrder.Status = "0";


            _context.Add(furnProductOrder);
             _context.SaveChanges();
            var products = _context.FurnProductOrders.Where(p => p.Order.UserId == num && p.Status =="0").Include(p => p.Order.User).Include(x => x.Product).ToList();
            return RedirectToAction("addToCart","Customer");
        }

        public IActionResult addToCart()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            decimal num = (decimal)HttpContext.Session.GetInt32("id");
            var products = _context.FurnProductOrders.Where(p => p.Order.UserId == num && p.Status == "0").Include(p => p.Order.User).Include(x => x.Product).ToList();
            ViewBag.total = products.Sum(p => p.Product.Price);
            return View(products);
        }
        public IActionResult checkCard()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> checkCard([Bind("Id,CardNumber,Cvv,Amount")] FurnBank furnBank, decimal id)
        {

            if (ModelState.IsValid)
            {
                var hisAccount = await _context.FurnBanks.Where(p => p.CardNumber == furnBank.CardNumber && p.Cvv == furnBank.Cvv).FirstOrDefaultAsync();
                if (hisAccount != null)
                {
                    decimal num = (decimal)HttpContext.Session.GetInt32("id");
                    var products = _context.FurnProductOrders.Where(p => p.Order.UserId == num && p.Status == "0");

                    FurnPayment furnPayment = new FurnPayment();
                    furnPayment.Paydate = DateTime.Now;
                    var res = products.Sum(p => p.Product.Price);
                    furnPayment.Amount = res;
                    furnPayment.UserId = num;
                    if(hisAccount.Amount >= furnPayment.Amount)
                    {
                        hisAccount.Amount -= furnPayment.Amount;
                        
                        var uss = _context.FurnUserAccounts.Where(p => p.Id == num).FirstOrDefault();
                        
                        string time = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
                        var card1 = _context.FurnProductOrders.Where(p => p.Order.UserId == num && p.Status == "0").Include(p => p.Order.User).Include(p => p.Product.Category).Include(p => p.Product).ToList();
                        Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 50f, 40f);

                        PdfWriter pw = PdfWriter.GetInstance(document, new FileStream("Invoice.pdf", FileMode.Create));
                        pw.PageEvent = new HeaderFooter();
                        document.Open();

                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;

                        PdfPCell _celll = new PdfPCell();
                        _celll = new PdfPCell(new Paragraph("Product Name"));
                        _celll.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(_celll);

                        _celll = new PdfPCell(new Paragraph("Product Price"));
                        _celll.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(_celll);

                        _celll = new PdfPCell(new Paragraph("Category Name"));
                        _celll.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(_celll);

                        _celll = new PdfPCell(new Paragraph("Date"));
                        _celll.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(_celll);

                        foreach (var item in card1)
                        {
                            PdfPCell _cell = new PdfPCell();

                            _cell = new PdfPCell(new Paragraph(item.Product.Namee));
                            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(_cell);


                            _cell = new PdfPCell(new Paragraph(item.Product.Price.ToString() + "JD"));
                            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(_cell);

                            _cell = new PdfPCell(new Paragraph(item.Product.Category.CategoryName.ToString()));
                            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(_cell);

                            _cell = new PdfPCell(new Paragraph(item.Order.Datee.ToString()));
                            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(_cell);


                        }

                        document.Add(table);
                        document.Close();

                        try
                        {
                            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);

                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com", 587);
                            mail.From = new MailAddress("furnworld1@outlook.com");
                            mail.To.Add(uss.Email);
                            mail.Subject = "Purchase Invoice";
                            mail.Body = "Ghayath";
                            smtp.Credentials = new NetworkCredential("furnworld1@outlook.com", "vmfkgnuswbfrhpxd");
                            mail.Attachments.Add(new Attachment("Invoice.pdf"));
                            smtp.EnableSsl = true;

                            smtp.Send(mail);

                            foreach (var item in products)
                            {
                                item.Status = "1";
                            }

                            _context.Add(furnPayment);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            throw ex.GetBaseException();
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Your amount not enough. Please try again.");
                        return RedirectToAction("checkCard");
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Your card is not valid. Please try again.");
                    return RedirectToAction("checkCard");
                }
                
            }
            return View(furnBank);
        }

        public async Task<IActionResult> RemoveProduct(decimal Id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var order = await _context.FurnProductOrders.FindAsync(Id);
            _context.FurnProductOrders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("addToCart");
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
