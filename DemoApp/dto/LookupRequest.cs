using System.ComponentModel.DataAnnotations;

namespace DemoApp.dto
{
    public class LookupRequest
    {
        [Required(ErrorMessage = "Mã số thuế là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã số thuế không hợp lệ!")]
        public string MaSoThue { get; set; } = string.Empty;

        [Required(ErrorMessage = "Captcha là bắt buộc")]
        [StringLength(10, ErrorMessage = "Captcha không đúng")]
        public string Captcha { get; set; } = string.Empty;
    }
}
