# DemoApp - API Tra cứu thông tin doanh nghiệp

## Mô tả
API này cho phép tra cứu thông tin doanh nghiệp dựa vào mã số thuế và captcha.

## Cài đặt và chạy

### 1. Cài đặt dependencies
```bash
dotnet restore
```

### 2. Cấu hình database
- Đảm bảo SQL Server đang chạy
- Chạy script SQL trong file `Scripts/CreateEnterpriseTable.sql` để tạo bảng và dữ liệu mẫu
- Hoặc để ứng dụng tự động tạo database (đã cấu hình trong Program.cs)

### 3. Chạy ứng dụng
```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `https://localhost:7000` (hoặc port khác tùy cấu hình)

## API Endpoints

### 1. Tra cứu thông tin doanh nghiệp
**POST** `/api/tax/lookup`

**Request Body:**
```json
{
  "maSoThue": "0310346199",
  "captcha": ""
}
```

**Response thành công:**
```json
{
  "success": true,
  "message": "Lấy thông tin doanh nghiệp thành công",
  "data": {
    "id": 5,
    "taxCode": "0310346199",
    "companyName": "CÔNG TY CỔ PHẦN PHẦN MỀM LINKQ",
    "address": "311G07, Đường số 8, Khu phố 24, Phường Bình Trưng, TP Hồ Chí Minh",
    "representative": "Thuế cơ sở 2 Thành phố Hồ Chí Minh",
    "status": "NNT đang hoạt động",
    "createdDate": "2025-09-04T11:37:52.6766667",
    "updatedDate": null
  }
}
```

**Response lỗi:**
```json
{
  "success": false,
  "message": "Không tìm thấy doanh nghiệp với mã số thuế này",
  "data": null
}
```

### 2. Lấy tất cả doanh nghiệp (để test)
**GET** `/api/tax/all`

## Dữ liệu mẫu
Bảng `Enterprises` đã có sẵn dữ liệu mẫu:
- Mã số thuế: `0310346199` - CÔNG TY CỔ PHẦN PHẦN MỀM LINKQ
- Mã số thuế: `0123456789` - Công ty TNHH ABC
- Mã số thuế: `0987654321` - Công ty Cổ phần XYZ
- Mã số thuế: `1111111111` - Công ty TNHH DEF
- Mã số thuế: `2222222222` - Công ty Cổ phần GHI

## Captcha
Truy cập `https://localhost:7000/api/tax/captcha` để lấy mã captcha.

## Swagger UI
Truy cập `https://localhost:7000/swagger` để xem và test API trực tiếp.

## Lưu ý
- Đảm bảo SQL Server đang chạy và có thể kết nối
- Connection string được cấu hình trong `appsettings.json`
- Ứng dụng sẽ tự động tạo database nếu chưa tồn tại
