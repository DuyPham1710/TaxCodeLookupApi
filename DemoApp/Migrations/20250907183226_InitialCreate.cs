using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyInfos",
                columns: table => new
                {
                    TaxID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TaxAuthority = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InternationalName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FoundingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagingBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CompanyType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MainIndustry = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfos", x => x.TaxID);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "Id", "Address", "CompanyName", "CreatedDate", "Representative", "Status", "TaxCode", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "123 Đường ABC, Quận 1, TP.HCM", "Công ty TNHH ABC", new DateTime(2025, 9, 8, 1, 32, 26, 659, DateTimeKind.Local).AddTicks(6575), "Nguyễn Văn A", "Hoạt động", "0123456789", null },
                    { 2, "456 Đường XYZ, Quận 2, TP.HCM", "Công ty Cổ phần XYZ", new DateTime(2025, 9, 8, 1, 32, 26, 659, DateTimeKind.Local).AddTicks(6577), "Trần Thị B", "Hoạt động", "0987654321", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfos_TaxID",
                table: "CompanyInfos",
                column: "TaxID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_TaxCode",
                table: "Enterprises",
                column: "TaxCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfos");

            migrationBuilder.DropTable(
                name: "Enterprises");
        }
    }
}
