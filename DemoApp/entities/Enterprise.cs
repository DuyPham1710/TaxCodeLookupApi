using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp.entities
{
    [Table("Enterprises")]
    public class Enterprise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TaxCode")]
        public string TaxCode { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        [Column("CompanyName")]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(1000)]
        [Column("Address")]
        public string Address { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("Representative")]
        public string Representative { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("Status")]
        public string Status { get; set; } = string.Empty;

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
