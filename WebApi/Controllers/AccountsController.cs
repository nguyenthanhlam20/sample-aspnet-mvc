using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Requests;

namespace WebApi.Controllers;

[Route("[Controller]/[Action]")]
public class AccountsController : ControllerBase
{
    private readonly ClientDbContext _context;
    public AccountsController(ClientDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] PagingRequest request)
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

        return Ok(request);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Account account)
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

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id);
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] Account account)
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
            return Ok("Success");
        }
        return BadRequest();
    }
}


