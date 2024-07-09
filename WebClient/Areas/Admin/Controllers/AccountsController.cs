using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Requests;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    public class AccountsController : Controller
    {
        private readonly ClientDbContext _context;
        public AccountsController(ClientDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PagingRequest request)
        {
            var searchTerm = request.SearchTerm;
            if (string.IsNullOrEmpty(searchTerm)) searchTerm = "";

            var accounts = await _context.Accounts
                .Where(x => x.Role != "Admin")
                .Where(x => (x.Fullname ?? "").Contains(searchTerm)
                || x.Email.Contains(searchTerm)) 
                .ToListAsync();

            request.TotalRecord = accounts.Count();
            request.TotalPages = (int)Math.Ceiling(accounts.Count() / (double)request.PageSize);
            accounts = accounts.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            accounts = accounts.OrderByDescending(a => a.Id).ToList();
            request.Items = accounts;

            return View(request);
        }


        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                var anyAccount = await _context.Accounts.AnyAsync(x => x.Email == account.Email);
                if(!anyAccount)
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Account account)
        {
            if (ModelState.IsValid)
            {
                var exist = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == account.Id);
                if (exist != null)
                {
                    exist.Fullname = account.Fullname;
                    exist.Address = account.Address;
                    exist.Avatar = account.Avatar;
                    exist.StudentCode = account.StudentCode;
                    exist.Major = account.Major;
                    exist.Bio = account.Bio;
                    exist.Phone = account.Phone;
                    exist.Campus = account.Campus;
                    exist.Cccd = account.Cccd;
                    exist.Class = account.Class;
                    exist.DateOfBirth = account.DateOfBirth;
                    exist.Gender = account.Gender;

                    _context.Accounts.Update(exist);
                    await _context.SaveChangesAsync();
                    ToastHelper.ShowSuccess(TempData, "Chỉnh sửa thông tin người dùng thành công.");
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(account);
        }
    }
}

