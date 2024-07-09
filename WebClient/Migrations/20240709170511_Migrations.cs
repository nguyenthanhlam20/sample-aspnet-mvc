using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebClient.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cccd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Campus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Address", "Avatar", "Bio", "Campus", "Cccd", "Class", "DateOfBirth", "Email", "Fullname", "Gender", "Major", "Password", "Phone", "Role", "StudentCode" },
                values: new object[] { 1, null, null, null, null, null, null, null, "admin@gmail.com", "Admin", null, null, "admin", null, "Admin", null });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Address", "Avatar", "Bio", "Campus", "Cccd", "Class", "DateOfBirth", "Email", "Fullname", "Gender", "Major", "Password", "Phone", "Role", "StudentCode" },
                values: new object[] { 2, "Hà Nội", "https://marketplace.canva.com/EAFdII60dSs/1/0/1600w/canva-lilac-brown-illustrative-cute-anime-girl-avatar-8GSg5pXqpk8.jpg", "Chào bạn.", "Hòa Lạc", "0303031124312", ".NET", new DateTime(2004, 7, 10, 0, 5, 11, 440, DateTimeKind.Local).AddTicks(2030), "linhnthe151434@fpt.edu.vn", "Nguyễn Thùy Linh", "Nữ", "Software Engineering", "lamnthe151515", "0385971809", "User", "HE151434" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
