using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp.entities
{
    [Table("CompanyInfos")]
    public class CompanyInfo
    {
        [Key]
        [StringLength(20)]
        public string TaxID { get; set; } = string.Empty;              // Mã số thuế

        [StringLength(500)]
        public string Name { get; set; } = string.Empty;               // Tên công ty

        [StringLength(500)]
        public string TaxAuthority { get; set; } = string.Empty;       // Địa chỉ thuế

        [StringLength(1000)]
        public string Address { get; set; } = string.Empty;            // Địa chỉ

        [StringLength(200)]
        public string Status { get; set; } = string.Empty;             // Tình trạng

        [StringLength(500)]
        public string InternationalName { get; set; } = string.Empty;  // Tên quốc tế

        [StringLength(200)]
        public string ShortName { get; set; } = string.Empty;          // Tên viết tắt

        [StringLength(200)]
        public string Representative { get; set; } = string.Empty;     // Người đại diện

        [StringLength(50)]
        public string Telephone { get; set; } = string.Empty;          // Số điện thoại

        public string FoundingDate { get; set; } = string.Empty;       // Ngày hoạt động

        [StringLength(500)]
        public string ManagingBy { get; set; } = string.Empty;         // Quản lý bởi

        [StringLength(200)]
        public string CompanyType { get; set; } = string.Empty;        // Loại hình DN

        [StringLength(500)]
        public string MainIndustry { get; set; } = string.Empty;       // Ngành nghề chính

    }
}
