using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[Controller]/[Action]")]
    public class AuthenController : Controller
    {
        private readonly ClientDbContext _context;

        public AuthenController(ClientDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Account account)
        {
            var anyAccount = await _context.Accounts.AnyAsync(x => x.Email == account.Email);
            if (!anyAccount)
            {
                account.Role = "User";
                account.Avatar = "https://www.shutterstock.com/image-vector/default-avatar-profile-icon-social-600nw-1677509740.jpg";
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]Account account)
        {
            var exist = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == account.Email && x.Password == account.Password);
            if (exist != null) return Ok(exist);
            return BadRequest();
        }
    }
}
