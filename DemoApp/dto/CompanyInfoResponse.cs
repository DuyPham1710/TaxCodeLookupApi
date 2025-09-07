using DemoApp.entities;

namespace DemoApp.dto
{
    public class CompanyInfoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public CompanyInfo? Data { get; set; }
    }
}
