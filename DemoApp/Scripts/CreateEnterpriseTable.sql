USE Enterprise;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Enterprises' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Enterprises](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [TaxCode] [nvarchar](20) NOT NULL,
        [CompanyName] [nvarchar](500) NOT NULL,
        [Address] [nvarchar](1000) NULL,
        [Representative] [nvarchar](200) NULL,
        [Status] [nvarchar](100) NULL,
        [CreatedDate] [datetime2](7) NOT NULL,
        [UpdatedDate] [datetime2](7) NULL,
        CONSTRAINT [PK_Enterprises] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    
    -- Tạo unique index cho TaxCode
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Enterprises_TaxCode] ON [dbo].[Enterprises]
    (
        [TaxCode] ASC
    );
    
    PRINT 'Bảng Enterprises đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT 'Bảng Enterprises đã tồn tại!';
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Enterprises] WHERE [TaxCode] = '0123456789')
BEGIN
    INSERT INTO [dbo].[Enterprises] ([TaxCode], [CompanyName], [Address], [Representative], [Status], [CreatedDate])
    VALUES 
    ('0310346199', N'CÔNG TY CỔ PHẦN PHẦN MỀM LINKQ', N'311G07, Đường số 8, Khu phố 24, Phường Bình Trưng, TP Hồ Chí Minh', N'Thuế cơ sở 2 Thành phố Hồ Chí Minh', N'NNT đang hoạt động', GETDATE()),
    ('0123456789', N'Công ty TNHH ABC', N'123 Đường ABC, Quận 1, TP.HCM', N'Nguyễn Văn A', N'Hoạt động', GETDATE()),
    ('0987654321', N'Công ty Cổ phần XYZ', N'456 Đường XYZ, Quận 2, TP.HCM', N'Trần Thị B', N'Hoạt động', GETDATE()),
    ('1111111111', N'Công ty TNHH DEF', N'789 Đường DEF, Quận 3, TP.HCM', N'Lê Văn C', N'Hoạt động', GETDATE()),
    ('2222222222', N'Công ty Cổ phần GHI', N'321 Đường GHI, Quận 4, TP.HCM', N'Phạm Thị D', N'Tạm ngừng', GETDATE());
    
    PRINT 'Dữ liệu mẫu đã được thêm thành công!';
END
ELSE
BEGIN
    PRINT 'Dữ liệu mẫu đã tồn tại!';
END
GO

SELECT * FROM [dbo].[Enterprises];
GO
