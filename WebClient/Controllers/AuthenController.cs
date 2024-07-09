using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using WebClient.Helpers;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ClientDbContext _context;

        public AuthenController(ClientDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var anyAccount = await _context.Accounts.AnyAsync(x => x.Email == account.Email);
                    if (!anyAccount)
                    {
                        account.Role = "User";
                        account.Avatar = "https://www.shutterstock.com/image-vector/default-avatar-profile-icon-social-600nw-1677509740.jpg";
                        _context.Accounts.Add(account);
                        await _context.SaveChangesAsync();
                        ToastHelper.ShowSuccess(TempData, "Tạo mới người dùng thành công.");
                        return RedirectToAction(nameof(Index));
                    }
                    ToastHelper.ShowError(TempData, "Người dùng có email " + account.Email + "đã tồn tại trong hệ thống.");
                }
                return View(account);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Account account)
        {
            try
            {
                var exist = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == account.Email && x.Password == account.Password);
                if (exist == null) throw new Exception("Tài khoản hoặc mật khẩu không chính xác.");
                SessionHelper.SetObject<Account>(HttpContext.Session, "Account", exist);
                ToastHelper.ShowSuccess(TempData, "Đăng nhập thành công.");

                // Redirect the user based on their role
                switch (exist.Role)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    case "User":
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    default:
                        return View("Views/Home/Index.cshtml");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(account);
        }


        [HttpGet]
        [Route("/SignOut")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                SessionHelper.Remove(HttpContext.Session, "Account");
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return RedirectToAction("Index", "Home"); 
        }
    }
}
