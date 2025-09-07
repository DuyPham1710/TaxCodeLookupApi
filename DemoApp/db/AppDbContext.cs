using Microsoft.EntityFrameworkCore;
using DemoApp.entities;

namespace DemoApp.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tạo index cho TaxCode để tìm kiếm nhanh hơn
            modelBuilder.Entity<Enterprise>()
                .HasIndex(e => e.TaxCode)
                .IsUnique();

            modelBuilder.Entity<CompanyInfo>()
                .HasIndex(c => c.TaxID)
                .IsUnique();

            modelBuilder.Entity<Enterprise>().HasData(
                new Enterprise
                {
                    Id = 1,
                    TaxCode = "0123456789",
                    CompanyName = "Công ty TNHH ABC",
                    Address = "123 Đường ABC, Quận 1, TP.HCM",
                    Representative = "Nguyễn Văn A",
                    Status = "Hoạt động",
                    CreatedDate = DateTime.Now
                },
                new Enterprise
                {
                    Id = 2,
                    TaxCode = "0987654321",
                    CompanyName = "Công ty Cổ phần XYZ",
                    Address = "456 Đường XYZ, Quận 2, TP.HCM",
                    Representative = "Trần Thị B",
                    Status = "Hoạt động",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
