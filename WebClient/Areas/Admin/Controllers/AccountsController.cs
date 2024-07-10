using Microsoft.AspNetCore.Mvc;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Requests;
using WebClient.Services;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class AccountsController : Controller
    {
        private readonly ClientService _service;

        public AccountsController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PagingRequest request)
        {
            var searchTerm = request.SearchTerm;
            if (string.IsNullOrEmpty(searchTerm)) request.SearchTerm = "";
            var response = await _service.Post<PagingRequest>("Accounts/GetAll", request);
            return View(response);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.Post("Accounts/Create", account);
                if (response != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Tạo mới người dùng thành công.");
                    return RedirectToAction(nameof(Index));
                }
                ToastHelper.ShowError(TempData, "Người dùng có email " + account.Email + "đã tồn tại trong hệ thống.");
            }
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var account = await _service.Get<Account>("Accounts/GetById?id=" + id);
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var account = await _service.Get<Account>("Accounts/GetById?id=" + id);
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

