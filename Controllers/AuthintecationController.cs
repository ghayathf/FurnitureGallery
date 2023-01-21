using FurnitureShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace FurnitureShop.Controllers
{
    public class AuthintecationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthintecationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fullname,Phone,ImagePath,Email,RoleId,ImageFile")] FurnUserAccount furnUserAccount, string password, string rePassword2)
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
                furnUserAccount.RoleId = 2;
                _context.Add(furnUserAccount);
                await _context.SaveChangesAsync();

                FurnLogin furnLogin = new FurnLogin();
                furnLogin.Username = furnUserAccount.Email;
                furnLogin.Passwordd = EncryptPassword(password);
                furnLogin.UserId = furnUserAccount.Id;

                _context.Add(furnLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Authintecation");
            }
            ViewData["RoleId"] = new SelectList(_context.FurnRoles, "Id", "Rolename", furnUserAccount.RoleId);
            return View(furnUserAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Passwordd,UserId")] FurnLogin furnLogin)
        {
            
            var auth = _context.FurnLogins.Where(x => x.Username == furnLogin.Username &&
            x.Passwordd == EncryptPassword(furnLogin.Passwordd)).FirstOrDefault();
            if (auth != null)
            {
                var userr = _context.FurnUserAccounts.Where(x => x.Email == auth.Username).FirstOrDefault();
                switch (userr.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("id", (int)userr.Id);
                        HttpContext.Session.SetString("usernameee", userr.Fullname);
                        HttpContext.Session.SetString("Photo", userr.ImagePath);
                        HttpContext.Session.SetInt32("Login", 1);
                        return RedirectToAction("Index", "Admin");
                    case 2:
                        HttpContext.Session.SetInt32("id", (int)userr.Id);
                        HttpContext.Session.SetString("usernameee", userr.Fullname);
                        HttpContext.Session.SetString("Photo", userr.ImagePath);
                        HttpContext.Session.SetInt32("Login", 1);
                        return RedirectToAction("Index", "Customer");
                }


            }
            else
            {
                ViewBag.flag = 0;
            }

            return View();

        }
    }
}
