CREATE TABLE Categories
(
    CategoryId       INT IDENTITY(1,1) NOT NULL,
    Name             NVARCHAR(150) NOT NULL,
    Slug             NVARCHAR(180) NOT NULL,
    Description      NVARCHAR(1000) NULL,
    ImageUrl         NVARCHAR(500) NULL,
    IsActive         BIT NOT NULL CONSTRAINT DF_Categories_IsActive DEFAULT (1),
    DisplayOrder     INT NOT NULL CONSTRAINT DF_Categories_DisplayOrder DEFAULT (0),
    CreatedAt        DATETIME2(0) NOT NULL CONSTRAINT DF_Categories_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt        DATETIME2(0) NULL, CONSTRAINT PK_Categories PRIMARY KEY (CategoryId), CONSTRAINT UQ_Categories_Slug UNIQUE (Slug)
);

CREATE TABLE Brands
(
    BrandId          INT IDENTITY(1,1) NOT NULL,
    Name             NVARCHAR(150) NOT NULL,
    Slug             NVARCHAR(180) NOT NULL,
    WebsiteUrl       NVARCHAR(500) NULL,
    ManufacturerCode NVARCHAR(100) NULL,
    Description      NVARCHAR(1000) NULL,
    LogoUrl          NVARCHAR(500) NULL,
    IsActive         BIT NOT NULL CONSTRAINT DF_Brands_IsActive DEFAULT (1),
    DisplayOrder     INT NOT NULL CONSTRAINT DF_Brands_DisplayOrder DEFAULT (0),
    CreatedAt        DATETIME2(0) NOT NULL CONSTRAINT DF_Brands_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt        DATETIME2(0) NULL, 
    CONSTRAINT PK_Brands PRIMARY KEY (BrandId),CONSTRAINT UQ_Brands_Slug UNIQUE (Slug)
);

CREATE TABLE Products
(
    ProductId                    INT IDENTITY(1,1) NOT NULL,
    CategoryId                   INT NOT NULL,
    BrandId                      INT NULL,
    Name                         NVARCHAR(250) NOT NULL,
    Slug                         NVARCHAR(300) NOT NULL,
    PartNumber                   NVARCHAR(150) NULL,
    ModelNumber                  NVARCHAR(150) NULL,
    ShortDescription             NVARCHAR(1000) NULL,
    Description                  NVARCHAR(MAX) NULL,
    IsFeatured                   BIT NOT NULL CONSTRAINT DF_Products_IsFeatured DEFAULT (0),
    IsActive                     BIT NOT NULL CONSTRAINT DF_Products_IsActive DEFAULT (1),
    DisplayOrder                 INT NOT NULL CONSTRAINT DF_Products_DisplayOrder DEFAULT (0),
    CreatedAt                    DATETIME2(0) NOT NULL CONSTRAINT DF_Products_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt                    DATETIME2(0) NULL,
    LastSyncedAt                 DATETIME2(0) NULL,
    CONSTRAINT PK_Products PRIMARY KEY (ProductId),CONSTRAINT UQ_Products_Slug UNIQUE (Slug),
    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (CategoryId)
        REFERENCES Categories(CategoryId),
    CONSTRAINT FK_Products_Brands
        FOREIGN KEY (BrandId)
        REFERENCES Brands(BrandId)
);


CREATE TABLE ProductSpecifications
(
    ProductSpecificationId    INT IDENTITY(1,1) NOT NULL,
    ProductId                 INT NOT NULL,
    SpecificationName         NVARCHAR(150) NOT NULL,
    SpecificationValue        NVARCHAR(1000) NOT NULL,
    DisplayOrder              INT NOT NULL CONSTRAINT DF_ProductSpecifications_DisplayOrder DEFAULT (0),
    CreatedAt                 DATETIME2(0) NOT NULL CONSTRAINT DF_ProductSpecifications_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt                 DATETIME2(0) NULL,
    CONSTRAINT PK_ProductSpecifications PRIMARY KEY (ProductSpecificationId),
    CONSTRAINT FK_ProductSpecifications_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);


CREATE TABLE ProductImages
(
    ProductImageId       INT IDENTITY(1,1) NOT NULL,
    ProductId            INT NOT NULL,
    ImageUrl             NVARCHAR(1000) NOT NULL,
    AltText              NVARCHAR(250) NULL,
    IsPrimary            BIT NOT NULL CONSTRAINT DF_ProductImages_IsPrimary DEFAULT (0),
    DisplayOrder         INT NOT NULL CONSTRAINT DF_ProductImages_DisplayOrder DEFAULT (0),
    CreatedAt            DATETIME2(0) NOT NULL CONSTRAINT DF_ProductImages_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_ProductImages PRIMARY KEY (ProductImageId),
    CONSTRAINT FK_ProductImages_Products  FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE ProductSources
(
    ProductSourceId        INT IDENTITY(1,1) NOT NULL,
    ProductId              INT NOT NULL,
    SourceType             NVARCHAR(30) NOT NULL,
    SourceUrl              NVARCHAR(1000) NOT NULL,
    ExternalProductId      NVARCHAR(200) NULL,
    ExternalProductCode    NVARCHAR(200) NULL,
    LastFetchedAt          DATETIME2(0) NULL,
    LastModifiedAt         DATETIME2(0) NULL,
    SyncStatus             NVARCHAR(30) NOT NULL CONSTRAINT DF_ProductSources_SyncStatus DEFAULT ('Pending'),
    SyncMessage            NVARCHAR(2000) NULL,
    CreatedAt              DATETIME2(0) NOT NULL CONSTRAINT DF_ProductSources_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt              DATETIME2(0) NULL, CONSTRAINT PK_ProductSources PRIMARY KEY (ProductSourceId),
    CONSTRAINT FK_ProductSources_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT CK_ProductSources_SourceType
        CHECK
        (
            SourceType IN
            (
                'Manufacturer',
                'Distributor',
                'Manual'
            )
        ),

    CONSTRAINT CK_ProductSources_SyncStatus
        CHECK
        (
            SyncStatus IN
            (
                'Pending',
                'Success',
                'Failed',
                'Disabled'
            )
        )
);

CREATE TABLE ProductSourceData
(
    ProductSourceDataId     BIGINT IDENTITY(1,1) NOT NULL,
    ProductSourceId         INT NOT NULL,
    RawJson                 NVARCHAR(MAX) NOT NULL,
    FetchedAt               DATETIME2(0) NOT NULL CONSTRAINT DF_ProductSourceData_FetchedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_ProductSourceData PRIMARY KEY (ProductSourceDataId),
    CONSTRAINT FK_ProductSourceData_ProductSources FOREIGN KEY (ProductSourceId) REFERENCES ProductSources(ProductSourceId),
    CONSTRAINT CK_ProductSourceData_RawJson CHECK (ISJSON(RawJson) = 1)
);


CREATE TABLE QuoteEnquiries
(
    QuoteEnquiryId       INT IDENTITY(1,1) NOT NULL,
    QuoteNumber          NVARCHAR(30) NOT NULL,
    CustomerName         NVARCHAR(150) NOT NULL,
    CompanyName          NVARCHAR(200) NULL,
    Email                NVARCHAR(254) NOT NULL,
    Phone                NVARCHAR(30) NOT NULL,
    Location             NVARCHAR(200) NULL,
    Message              NVARCHAR(2000) NULL,
    Status               NVARCHAR(30) NOT NULL CONSTRAINT DF_QuoteEnquiries_Status DEFAULT ('New'),
    CreatedAt            DATETIME2(0) NOT NULL CONSTRAINT DF_QuoteEnquiries_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt            DATETIME2(0) NULL,
    CONSTRAINT PK_QuoteEnquiries PRIMARY KEY (QuoteEnquiryId),
    CONSTRAINT UQ_QuoteEnquiries_QuoteNumber UNIQUE (QuoteNumber),
    CONSTRAINT CK_QuoteEnquiries_Status
        CHECK
        (
            Status IN
            (
                'New',
                'InProgress',
                'Quoted',
                'Closed',
                'Cancelled'
            )
        )
);

CREATE TABLE QuoteEnquiryItems
(
    QuoteEnquiryItemId     INT IDENTITY(1,1) NOT NULL,
    QuoteEnquiryId         INT NOT NULL,
    ProductId              INT NULL,
    /*
        Snapshot of the product name at the time
        the enquiry was submitted.
    */
    ProductName            NVARCHAR(250) NOT NULL,
    Quantity               DECIMAL(18,2) NULL,
    Unit                   NVARCHAR(50) NULL,
    CustomerRequirement    NVARCHAR(1000) NULL,
    CreatedAt              DATETIME2(0) NOT NULL CONSTRAINT DF_QuoteEnquiryItems_CreatedAt DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT PK_QuoteEnquiryItems PRIMARY KEY (QuoteEnquiryItemId),
    CONSTRAINT FK_QuoteEnquiryItems_QuoteEnquiries FOREIGN KEY (QuoteEnquiryId) REFERENCES QuoteEnquiries(QuoteEnquiryId),
    CONSTRAINT FK_QuoteEnquiryItems_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE QuoteReplies
(
    QuoteReplyId        INT IDENTITY(1,1) NOT NULL,
    QuoteEnquiryId      INT NOT NULL,
    ReplyMessage        NVARCHAR(4000) NOT NULL,
    /*
        No User table/login system is required.
        This is simply the display name of the person/company
        responding to the enquiry.
    */
    RepliedBy           NVARCHAR(150) NOT NULL,
    CreatedAt            DATETIME2(0) NOT NULL  CONSTRAINT DF_QuoteReplies_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CONSTRAINT PK_QuoteReplies PRIMARY KEY (QuoteReplyId),
    CONSTRAINT FK_QuoteReplies_QuoteEnquiries FOREIGN KEY (QuoteEnquiryId) REFERENCES QuoteEnquiries(QuoteEnquiryId)
);




/* =========================================================
   INDEXES
   ========================================================= */

/* Products */

CREATE INDEX IX_Products_CategoryId
    ON Products(CategoryId);
GO

CREATE INDEX IX_Products_BrandId
    ON Products(BrandId);
GO

CREATE INDEX IX_Products_IsActive
    ON Products(IsActive);
GO

CREATE INDEX IX_Products_CategoryId_IsActive
    ON Products(CategoryId, IsActive);
GO

CREATE INDEX IX_Products_BrandId_IsActive
    ON Products(BrandId, IsActive);
GO

CREATE INDEX IX_Products_Name
    ON Products(Name);
GO


/* Product Specifications */

CREATE INDEX IX_ProductSpecifications_ProductId
    ON ProductSpecifications(ProductId);
GO


/* Product Images */

CREATE INDEX IX_ProductImages_ProductId
    ON ProductImages(ProductId);
GO


/* Product Sources */

CREATE INDEX IX_ProductSources_ProductId
    ON ProductSources(ProductId);
GO

CREATE INDEX IX_ProductSources_SyncStatus
    ON ProductSources(SyncStatus);
GO

CREATE INDEX IX_ProductSources_ExternalProductCode
    ON ProductSources(ExternalProductCode);
GO


/* Product Source Data */

CREATE INDEX IX_ProductSourceData_ProductSourceId
    ON ProductSourceData(ProductSourceId);
GO


/* Quote Enquiries */

CREATE INDEX IX_QuoteEnquiries_Status
    ON QuoteEnquiries(Status);
GO

CREATE INDEX IX_QuoteEnquiries_CreatedAt
    ON QuoteEnquiries(CreatedAt);
GO


/* Quote Enquiry Items */

CREATE INDEX IX_QuoteEnquiryItems_QuoteEnquiryId
    ON QuoteEnquiryItems(QuoteEnquiryId);
GO

CREATE INDEX IX_QuoteEnquiryItems_ProductId
    ON QuoteEnquiryItems(ProductId);
GO


/* Quote Replies */

CREATE INDEX IX_QuoteReplies_QuoteEnquiryId
    ON QuoteReplies(QuoteEnquiryId);
GO