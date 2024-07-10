using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Seeding
{
    public class AccountSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public AccountSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Fullname = "Admin",
                    Password = "admin",
                    Role = "Admin",
                },
               new Account()
               {
                   Id = 2,
                   Fullname = "Nguyễn Thùy Linh",
                   Password = "lamnthe151515",
                   Role = "User",
                   Avatar = "https://marketplace.canva.com/EAFdII60dSs/1/0/1600w/canva-lilac-brown-illustrative-cute-anime-girl-avatar-8GSg5pXqpk8.jpg",
                   Cccd = "0303031124312",
                   Gender = "Nữ",
                   Address = "Hà Nội",
                   Bio = "Chào bạn.",
                   DateOfBirth = DateTime.Now.AddYears(-20),
                   Campus = "Hòa Lạc",
                   Major = "Software Engineering",
                   Class = ".NET",
                   StudentCode = "HE151434",
                   Email = "linhnthe151434@fpt.edu.vn",
                   Phone = "0385971809",
               });
        }
    }
}
