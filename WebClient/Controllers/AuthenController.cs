using Microsoft.AspNetCore.Mvc;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ClientService _service;

        public AuthenController(ClientService service)
        {
            _service = service;
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
                    var response = await _service.Post("Authen/Register", account);
                    if(response != null)
                    {
                        ToastHelper.ShowSuccess(TempData, "Tạo mới tài khoản thành công.");
                        return RedirectToAction(nameof(SignIn));
                    }
                    ToastHelper.ShowError(TempData, "Người dùng có email " + account.Email + "đã tồn tại trong hệ thống.");
                }
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
                var response = await _service.Post<Account>("Authen/SignIn", account);
                if (response == null) throw new Exception("Tài khoản hoặc mật khẩu không chính xác.");
                SessionHelper.SetObject<Account>(HttpContext.Session, "Account", response);
                ToastHelper.ShowSuccess(TempData, "Đăng nhập thành công.");

                // Redirect the user based on their role
                switch (response.Role)
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
        public IActionResult LogOut()
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
