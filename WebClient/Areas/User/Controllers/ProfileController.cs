using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebClient.Helpers;
using WebClient.Models;

namespace WebClient.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[Controller]/[Action]")]
    public class ProfileController : Controller
    {
        private readonly ClientDbContext _context;
        public ProfileController(ClientDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Account userInfo = SessionHelper.GetObject<Account>(HttpContext.Session, "Account");
            var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == userInfo.Id);
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Account userInfo = SessionHelper.GetObject<Account>(HttpContext.Session, "Account");
            var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == userInfo.Id);
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
                    ToastHelper.ShowSuccess(TempData, "Chỉnh sửa thông tin cá nhân thành công.");
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(account);
        }
    }
}
