﻿using DemoApp.entities;

namespace DemoApp.Service
{
    public class CompanyService
    {
        public static async Task<CompanyInfo> GetCompanyInfo(string taxOrName)
        {
            var http = new HttpClient();

            // Thêm header giả lập trình duyệt
            http.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.0.0 Safari/537.36");
            http.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            http.DefaultRequestHeaders.Add("Accept-Language", "vi,en;q=0.9");
            http.DefaultRequestHeaders.Add("Referer", "https://masothue.com/");

            var url = $"https://masothue.com/Search/?q={Uri.EscapeDataString(taxOrName)}&type=auto&force-search=1";
            var html = await http.GetStringAsync(url);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var info = new CompanyInfo();

            // Tên công ty từ thẻ h1
          //  info.Name = doc.DocumentNode.SelectSingleNode("//h1[@class='h1']")?.InnerText.Trim();

            // Hoặc fallback lấy trong th/table (nếu có)
          //  if (string.IsNullOrEmpty(info.Name))
         //   {
                info.Name = doc.DocumentNode.SelectSingleNode("//th[@itemprop='name']/span")?.InnerText.Trim();
         //   }

            // Mã số thuế
            info.TaxID = doc.DocumentNode.SelectSingleNode("//td[@itemprop='taxID']/span")?.InnerText.Trim();

            // Địa chỉ thuế (dòng 1 address)
            info.TaxAuthority = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Địa chỉ Thuế')]]/td[2]/span")?.InnerText.Trim();

            // Địa chỉ (dòng 2 address)
            info.Address = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Địa chỉ')]][2]/td[2]/span")?.InnerText.Trim();

            // Tình trạng
            info.Status = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Tình trạng')]]/td[2]")?.InnerText.Trim();

            // Tên quốc tế
            info.InternationalName = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Tên quốc tế')]]/td[2]/span")?.InnerText.Trim();

            // Tên viết tắt
            info.ShortName = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Tên viết tắt')]]/td[2]/span")?.InnerText.Trim();

            // Người đại diện
            info.Representative = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Người đại diện')]]/td[2]//a")?.InnerText.Trim();

            // Điện thoại
            info.Telephone = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Điện thoại')]]/td[2]")?.InnerText.Trim();

            // Ngày hoạt động
            info.FoundingDate = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Ngày hoạt động')]]/td[2]/span")?.InnerText.Trim();

            // Quản lý bởi
            info.ManagingBy = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Quản lý bởi')]]/td[2]/span")?.InnerText.Trim();

            // Loại hình DN
            info.CompanyType = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Loại hình DN')]]/td[2]")?.InnerText.Trim();

            // Ngành nghề chính
            info.MainIndustry = doc.DocumentNode.SelectSingleNode("//tr[td[contains(text(),'Ngành nghề chính')]]/td[2]/a")?.InnerText.Trim();


            return info;
        }
    }
}
