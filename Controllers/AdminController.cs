using FurnitureShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace FurnitureShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hash);
            }
        }
        public IActionResult Index()
        {
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.count = _context.FurnUserAccounts.Count(x => x.RoleId == 2);
            ViewBag.prods = _context.FurnProducts.Count();
            ViewBag.orders = _context.FurnPayments.Sum(x => x.Amount);
            ViewBag.PayedOrders = _context.FurnProductOrders.Count(x => x.Status == "1");
            var tot = _context.FurnProductOrders.Where(p => p.Status == "1").Sum(p => p.Product.Price);
            var vals = _context.FurnProductOrders.Where(p => p.Status == "1").Sum(p => p.Product.Valuee);
            ViewBag.Profit = tot - vals;

            return View();
        }
        public IActionResult GuestsEmails()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var b = _context.FurnContactus.Where(p => p.Id != 1).ToList();
            return View(b);
        }
        public async Task<IActionResult> Products(int Id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.username = HttpContext.Session.GetString("username");
            var products = await _context.FurnProducts.Where(x => x.CategoryId == Id).Include(p => p.Category).ToListAsync();
            return View(products);
        }
        
        public JsonResult GetPieChartJSON()
        {
            var Paidoreds = _context.FurnProductOrders.Where(p => p.Status == "1").Count();
            var notpaid = _context.FurnProductOrders.Where(p => p.Status == "0").Count(); 
            List<BlogCharts> list = new List<BlogCharts>();
            list.Add(new BlogCharts { CategoryName = "Paid Orders", PostCount = Paidoreds });
            list.Add(new BlogCharts { CategoryName = "Not Paid Orders", PostCount = notpaid });
            return Json(new { JSONList = list });
        }
        public JsonResult GetPieChartJSONn()
        {
            var Bushra = _context.FurnPayments.Where(p => p.User.Id == 12).Count();
            var ahmad = _context.FurnPayments.Where(p => p.User.Id == 8).Count();
            var mohammad = _context.FurnPayments.Where(p => p.User.Id == 7).Count();
            var Boshraaaa = _context.FurnPayments.Where(p => p.User.Id == 22).Count();
            var ghh = _context.FurnPayments.Where(p => p.User.Id == 24).Count();
            List<BlogCharts> list = new List<BlogCharts>();
            list.Add(new BlogCharts { CategoryName = "Bushra", PostCount = Bushra });
            list.Add(new BlogCharts { CategoryName = "Ahmad", PostCount = ahmad });
            list.Add(new BlogCharts { CategoryName = "Mohammad", PostCount = mohammad });
            list.Add(new BlogCharts { CategoryName = "Boshraaaa", PostCount = Boshraaaa });
            list.Add(new BlogCharts { CategoryName = "Mohammad", PostCount = ghh });
            return Json(new { JSONList = list });
        }
        public JsonResult GetPieChartJSONnn()
        {
            var Jan = _context.FurnPayments.Where(p => p.Paydate.Value.Month == 1).Count();
            var Feb = _context.FurnPayments.Where(p => p.Paydate.Value.Month == 2).Count();
            var Mar = _context.FurnPayments.Where(p => p.Paydate.Value.Month == 3).Count();
            List<BlogCharts> list = new List<BlogCharts>();
            list.Add(new BlogCharts { CategoryName = "January", PostCount = Jan });
            list.Add(new BlogCharts { CategoryName = "Februery", PostCount = Feb });
            list.Add(new BlogCharts { CategoryName = "March", PostCount = Mar });
            return Json(new { JSONList = list });
        }
        public JsonResult GetPieChartJSONnnn()
        {
            var Orange = _context.FurnUserAccounts.Where(p => p.Phone.StartsWith("077")).Count();
            var Umniah = _context.FurnUserAccounts.Where(p => p.Phone.StartsWith("078")).Count();
            var Zain = _context.FurnUserAccounts.Where(p => p.Phone.StartsWith("079")).Count();
            List<BlogCharts> list = new List<BlogCharts>();
            list.Add(new BlogCharts { CategoryName = "Orange", PostCount = Orange });
            list.Add(new BlogCharts { CategoryName = "Umniah", PostCount = Umniah });
            list.Add(new BlogCharts { CategoryName = "Zain", PostCount = Zain });
            return Json(new { JSONList = list });
        }
        public JsonResult GetPieChartJSONnnnn()
        {
            var Tables = _context.FurnProductOrders.Where(p => p.Product.Category.Id == 1).Count();
            var Chairs = _context.FurnProductOrders.Where(p => p.Product.Category.Id == 2).Count();
            var Sofas = _context.FurnProductOrders.Where(p => p.Product.Category.Id == 3).Count();
            var Beds = _context.FurnProductOrders.Where(p => p.Product.Category.Id == 4).Count();

            List<BlogCharts> list = new List<BlogCharts>();
            list.Add(new BlogCharts { CategoryName = "Tables", PostCount = Tables });
            list.Add(new BlogCharts { CategoryName = "Chairs", PostCount = Chairs });
            list.Add(new BlogCharts { CategoryName = "Sofa's", PostCount = Sofas });
            list.Add(new BlogCharts { CategoryName = "Beds", PostCount = Beds });

            return Json(new { JSONList = list });
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
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.username = HttpContext.Session.GetString("username");

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
                    furnUserAccount.RoleId = 1;
                    _context.Update(furnUserAccount);
                    await _context.SaveChangesAsync();
                    var log = _context.FurnLogins.Where(x => x.UserId == furnUserAccount.Id).FirstOrDefault();
                    log.Username = furnUserAccount.Email;
                    log.Passwordd = EncryptPassword(pass);
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
                return RedirectToAction("ViewProfile", "Admin");
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

        public async Task<IActionResult> AllCustomers()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var modelContext = _context.FurnUserAccounts.Include(f => f.Role).Where(x => x.RoleId == 2);
            return View(await modelContext.ToListAsync());
        }

        //public async Task<IActionResult> Search(DateTime firstdate, DateTime enddate)
        //{
        //    var q = _context.FurnPayments
        //    .Where(n => n.Paydate >= firstdate)
        //    .Where(n => n.Paydate <= enddate);
        //    return View(q);
        //}


        
        public async Task<IActionResult> productsForCategory(int id)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var pros = await _context.FurnProducts.Where(x => x.CategoryId == id).Include(x => x.Category).ToListAsync();
            return View(pros);
        }

        public IActionResult AllPayments()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var pc = _context.FurnPayments.Include(p => p.User).ToList();
            return View(pc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllPayments(DateTime? stratDate, DateTime? endDate)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.username = HttpContext.Session.GetString("username");
            var pc = _context.FurnPayments.Include(p => p.User);
            if (stratDate == null && endDate == null)
            {
                return View(await pc.ToListAsync());
            }
            else if (stratDate == null && endDate != null)
            {
                var result = await pc.Where(p => p.Paydate.Value.Date <= endDate).ToListAsync();
                return View(result);
            }
            else if (stratDate != null && endDate == null)
            {
                var result1 = await pc.Where(p => p.Paydate.Value.Date >= stratDate).ToListAsync();
                return View(result1);
            }
            else if (stratDate == endDate && stratDate != null && endDate != null)
            {
                var ress = await pc.Where(p => p.Paydate.Value.Date == stratDate && p.Paydate.Value.Date == endDate).ToListAsync();
                return View(ress);
            }
            else
            {
                var result2 = await pc.Where(p => p.Paydate.Value.Date >= stratDate && p.Paydate.Value.Date <= endDate).ToListAsync();
                return View(result2);
            }
        }

        public IActionResult Report()
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            var model = _context.FurnProductOrders.Include(p => p.Product).Include(p => p.Order).Include(p => p.Order.User).Include(p=>p.Product.Category);
            ViewBag.total = _context.FurnPayments.Sum(p => p.Amount);
            return View(model);  
        }
        [HttpPost]
        public async Task<IActionResult> Report(DateTime? stratDate, DateTime? endDate)
        {
            ViewBag.Photo = HttpContext.Session.GetString("Photo");
            ViewBag.usernameee = HttpContext.Session.GetString("usernameee");
            ViewBag.username = HttpContext.Session.GetString("username");
            var pc = _context.FurnProductOrders.Include(p => p.Product).Include(p => p.Order).Include(p => p.Order.User).Include(p => p.Product.Category);
            if (stratDate == null && endDate == null)
            {
                return View(await pc.ToListAsync());
            }
            else if (stratDate == null && endDate != null)
            {
                var result = await pc.Where(p => p.Order.Datee.Value.Date <= endDate).ToListAsync();
                return View(result);
            }
            else if (stratDate != null && endDate == null)
            {
                var result1 = await pc.Where(p => p.Order.Datee.Value.Date >= stratDate).ToListAsync();
                return View(result1);
            }
            else if (stratDate == endDate && stratDate != null && endDate != null)
            {
                var ress = await pc.Where(p => p.Order.Datee.Value.Date == stratDate && p.Order.Datee.Value.Date == endDate).ToListAsync();
                return View(ress);
            }
            else
            {
                var result2 = await pc.Where(p => p.Order.Datee.Value.Date >= stratDate && p.Order.Datee.Value.Date <= endDate).ToListAsync();
                return View(result2);
            }

        }

        


        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        

    }
}
