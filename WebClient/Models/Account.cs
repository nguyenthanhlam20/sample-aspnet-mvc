using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Account
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Nhập tên đầy đủ của người dùng.")]
        public string Fullname { get; set; } = null!;

        [Required(ErrorMessage = "Nhập email của người dùng.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Nhập mật khẩu.")]
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
        public string? Avatar { get; set; }
        public string? Cccd { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Campus { get; set; }
        public string? Major { get; set; }
        public string? Class { get; set; }
        public string? StudentCode { get; set; }
    }
}
