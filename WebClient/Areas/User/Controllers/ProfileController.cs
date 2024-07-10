using Microsoft.AspNetCore.Mvc;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[Controller]/[Action]")]
    public class ProfileController : Controller
    {
        private readonly ClientService _service;
        public ProfileController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Account userInfo = SessionHelper.GetObject<Account>(HttpContext.Session, "Account");
            var account = await _service.Get<Account>("Accounts/GetById?id=" + userInfo.Id);
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            Account userInfo = SessionHelper.GetObject<Account>(HttpContext.Session, "Account");
            var account = await _service.Get<Account>("Accounts/GetById?id=" + userInfo.Id);
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Account account)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.Post("Accounts/Update", account);
                if (response != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Chỉnh sửa thông tin người dùng thành công.");
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(account);
        }
    }
}
