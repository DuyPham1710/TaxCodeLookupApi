using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApp.db;
using DemoApp.dto;
using DemoApp.entities;
using DemoApp.Service;
using System.Net;
using DemoApp.Constants;

namespace DemoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly AppDbContext _context;
        private static readonly Dictionary<string, CookieContainer> _sessions = new();
        public TaxController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<CompanyInfo>>> GetAllCompanyInfos()
        {
            try
            {
                var companyInfos = await _context.CompanyInfos.ToListAsync();
                return Ok(companyInfos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách thông tin doanh nghiệp");
            }
        }

        [HttpPost("save-enterprise")]
        public async Task<ActionResult<EnterpriseResponse>> SaveEnterprise([FromBody] CompanyInfo request)
        {
            if (request == null)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ" });
            }
            // kiểm tra trùng TaxID
            var exists = await _context.CompanyInfos.AnyAsync(c => c.TaxID == request.TaxID);
            if (exists)
            {
                return Conflict(new { success = false, message = "Mã số thuế đã tồn tại trong hệ thống" });
            }

            _context.CompanyInfos.Add(request);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Lưu thành công", companyInfo = request });
        }       

        [HttpPost("lookup-company")]
        public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfo([FromBody] LookupRequest request) 
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

                //if (!CaptchaService.ValidateCaptcha(request.Captcha))
                //{
                //    return BadRequest(new EnterpriseResponse
                //    {
                //        Success = false,
                //        Message = "Captcha không đúng",
                //        Data = null
                //    });
                //}
                if (!Request.Headers.TryGetValue("X-Session-Id", out var sessionId))
                    return BadRequest(new { success = false, message = "Thiếu X-Session-Id trong header" });

                if (!_sessions.TryGetValue(sessionId!, out var cookies))
                    return BadRequest(new { success = false, message = "Session không hợp lệ" });

                var companyInfo = await CompanyService.GetCompanyInfo(request.MaSoThue, request.Captcha, cookies);

                if (companyInfo == null)
                {
                    return NotFound(new CompanyInfoResponse
                    {
                        Success = false,
                        Message = "Không tìm thấy doanh nghiệp với mã số thuế này",
                        Data = null
                    });       
                }

                var response = new CompanyInfoResponse
                {
                    Success = true,
                    Message = "Lấy thông tin doanh nghiệp thành công",
                    Data = new CompanyInfo
                    {
                        TaxID = companyInfo.TaxID,
                        Name = companyInfo.Name,
                        Address = companyInfo.Address,
                        Representative = companyInfo.Representative,
                        Status = companyInfo.Status,
                        TaxAuthority = companyInfo.TaxAuthority,
                        InternationalName = companyInfo.InternationalName,
                        ShortName = companyInfo.ShortName,
                        Telephone = companyInfo.Telephone,
                        FoundingDate = companyInfo.FoundingDate,
                        ManagingBy = companyInfo.ManagingBy,
                        CompanyType = companyInfo.CompanyType,
                        MainIndustry = companyInfo.MainIndustry
                    }
                };
               // CaptchaService._currentCaptcha = null; // reset captcha after each request
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
        public async Task<IActionResult> GetCaptchaAsync()
        {
          
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler);

            var captchaUrl = $"{GlConstants.BASE_URL}/captcha.png?uid=" + Guid.NewGuid();
            var imgBytes = await client.GetByteArrayAsync(captchaUrl);

            var sessionId = Guid.NewGuid().ToString();
            _sessions[sessionId] = cookieContainer;

            Response.Headers["X-Session-Id"] = sessionId;
            return File(imgBytes, "image/png");
           
        }

        //[HttpGet("captcha-with-session")]
        //public async Task<IActionResult> GetCaptchaWithSessionAsync()
        //{
        //    var cookieContainer = new CookieContainer();
        //    var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        //    using var client = new HttpClient(handler);

        //    var captchaUrl = "https://tracuunnt.gdt.gov.vn/tcnnt/captcha.png?uid=" + Guid.NewGuid();
        //    var imgBytes = await client.GetByteArrayAsync(captchaUrl);

        //    var sessionId = Guid.NewGuid().ToString();
        //    _sessions[sessionId] = cookieContainer;

        //    Response.Headers["X-Session-Id"] = sessionId;
        //    return Ok(new
        //    {
        //        sessionId,
        //        captchaImage = Convert.ToBase64String(imgBytes)
        //    });
        //}

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
