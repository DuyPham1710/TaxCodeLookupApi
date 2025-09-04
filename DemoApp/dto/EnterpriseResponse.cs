namespace DemoApp.dto
{
    public class EnterpriseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public EnterpriseData? Data { get; set; }
    }

    public class EnterpriseData
    {
        public int Id { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Representative { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
