using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApp.db;
using DemoApp.dto;
using DemoApp.entities;
using DemoApp.Service;

namespace DemoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaxController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("lookup")]
        public async Task<ActionResult<EnterpriseResponse>> LookupEnterprise([FromBody] LookupRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new EnterpriseResponse
                    {
                        Success = false,
                        Message = "Dữ liệu đầu vào không hợp lệ",
                        Data = null
                    });
                }

                if (!CaptchaService.ValidateCaptcha(request.Captcha))
                {
                    return BadRequest(new EnterpriseResponse
                    {
                        Success = false,
                        Message = "Captcha không đúng",
                        Data = null
                    });
                }

                var enterprise = await _context.Enterprises
                    .FirstOrDefaultAsync(e => e.TaxCode == request.MaSoThue);

                if (enterprise == null)
                {
                    return NotFound(new EnterpriseResponse
                    {
                        Success = false,
                        Message = "Không tìm thấy doanh nghiệp với mã số thuế này",
                        Data = null
                    });
                }

                var response = new EnterpriseResponse
                {
                    Success = true,
                    Message = "Lấy thông tin doanh nghiệp thành công",
                    Data = new EnterpriseData
                    {
                        Id = enterprise.Id,
                        TaxCode = enterprise.TaxCode,
                        CompanyName = enterprise.CompanyName,
                        Address = enterprise.Address,
                        Representative = enterprise.Representative,
                        Status = enterprise.Status,
                        CreatedDate = enterprise.CreatedDate,
                        UpdatedDate = enterprise.UpdatedDate
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EnterpriseResponse
                {
                    Success = false,
                    Message = "Có lỗi xảy ra khi xử lý yêu cầu",
                    Data = null
                });
            }
        }

        [HttpGet("captcha")]
        public IActionResult GetCaptcha()
        {
            var captcha = CaptchaService.GenerateCaptcha();
            return Ok(new { captcha });
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<EnterpriseData>>> GetAllEnterprises()
        {
            try
            {
                var enterprises = await _context.Enterprises.ToListAsync();
                var result = enterprises.Select(e => new EnterpriseData
                {
                    Id = e.Id,
                    TaxCode = e.TaxCode,
                    CompanyName = e.CompanyName,
                    Address = e.Address,
                    Representative = e.Representative,
                    Status = e.Status,
                    CreatedDate = e.CreatedDate,
                    UpdatedDate = e.UpdatedDate
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {  
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách doanh nghiệp");
            }
        }
    }
}
