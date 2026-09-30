using System.Text.Json;
using Dapper;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.Core.Entities;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class QuoteEnquiryRepository : IQuoteEnquiryRepository
    {
        private readonly IDbConnectionProvider _provider;

        public QuoteEnquiryRepository(IDbConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<QuoteEnquiryListResponse> GetAllAsync(string? search, string? status, int page, int pageSize)
        {
            const string sql = @"
                SELECT
                    QuoteEnquiryId,
                    QuoteNumber,
                    CustomerName,
                    CompanyName,
                    Email,
                    Phone,
                    Location,
                    Status,
                    CreatedAt,
                    UpdatedAt
                FROM QuoteEnquiries
                WHERE
                    (
                        @Search IS NULL
                        OR @Search = ''
                        OR QuoteNumber LIKE '%' + @Search + '%'
                        OR CustomerName LIKE '%' + @Search + '%'
                        OR CompanyName LIKE '%' + @Search + '%'
                        OR Email LIKE '%' + @Search + '%'
                        OR Phone LIKE '%' + @Search + '%'
                    )
                    AND
                    (
                        @Status IS NULL
                        OR @Status = ''
                        OR Status = @Status
                    )
                ORDER BY CreatedAt DESC, QuoteEnquiryId DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;

                SELECT COUNT(1)
                FROM QuoteEnquiries
                WHERE
                    (
                        @Search IS NULL
                        OR @Search = ''
                        OR QuoteNumber LIKE '%' + @Search + '%'
                        OR CustomerName LIKE '%' + @Search + '%'
                        OR CompanyName LIKE '%' + @Search + '%'
                        OR Email LIKE '%' + @Search + '%'
                        OR Phone LIKE '%' + @Search + '%'
                    )
                    AND
                    (
                        @Status IS NULL
                        OR @Status = ''
                        OR Status = @Status
                    );";

            using var multi = await _provider.Connection.QueryMultipleAsync(
                sql,
                new
                {
                    Search = string.IsNullOrWhiteSpace(search) ? null : search.Trim(),
                    Status = string.IsNullOrWhiteSpace(status) ? null : status.Trim(),
                    Offset = (page - 1) * pageSize,
                    PageSize = pageSize
                });

            var items = (await multi.ReadAsync<QuoteEnquiryListItemResponse>()).ToList();
            var totalCount = await multi.ReadSingleAsync<int>();

            return new QuoteEnquiryListResponse
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<QuoteEnquiry> CreateAsync(CreateQuoteEnquiryRequest request)
        {
            const string sql = @"
                SET XACT_ABORT ON;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    INSERT INTO QuoteEnquiries
                    (
                        QuoteNumber,
                        CustomerName,
                        CompanyName,
                        Email,
                        Phone,
                        Location,
                        Message,
                        Status,
                        CreatedAt
                    )
                    VALUES
                    (
                        @QuoteNumber,
                        @CustomerName,
                        @CompanyName,
                        @Email,
                        @Phone,
                        @Location,
                        @Message,
                        'New',
                        SYSUTCDATETIME()
                    );

                    DECLARE @QuoteEnquiryId INT = CONVERT(INT, SCOPE_IDENTITY());

                    INSERT INTO QuoteEnquiryItems
                    (
                        QuoteEnquiryId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        CustomerRequirement,
                        CreatedAt
                    )
                    SELECT
                        @QuoteEnquiryId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        CustomerRequirement,
                        SYSUTCDATETIME()
                    FROM OPENJSON(@ItemsJson)
                    WITH
                    (
                        ProductId INT '$.ProductId',
                        ProductName NVARCHAR(250) '$.ProductName',
                        Quantity DECIMAL(18,2) '$.Quantity',
                        Unit NVARCHAR(50) '$.Unit',
                        CustomerRequirement NVARCHAR(1000) '$.CustomerRequirement'
                    );

                    COMMIT TRANSACTION;

                    SELECT
                        QuoteEnquiryId,
                        QuoteNumber,
                        CustomerName,
                        CompanyName,
                        Email,
                        Phone,
                        Location,
                        Message,
                        Status,
                        CreatedAt,
                        UpdatedAt
                    FROM QuoteEnquiries
                    WHERE QuoteEnquiryId = @QuoteEnquiryId;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK TRANSACTION;

                    THROW;
                END CATCH;";

            var quoteNumber = $"SE-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}"[..20];

            return await _provider.Connection.QuerySingleAsync<QuoteEnquiry>(
                sql,
                new
                {
                    QuoteNumber = quoteNumber,
                    request.CustomerName,
                    request.CompanyName,
                    request.Email,
                    request.Phone,
                    request.Location,
                    request.Message,
                    ItemsJson = JsonSerializer.Serialize(request.Items)
                });
        }

        public async Task<QuoteEnquiryResponse?> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT
                    QuoteEnquiryId,
                    QuoteNumber,
                    CustomerName,
                    CompanyName,
                    Email,
                    Phone,
                    Location,
                    Message,
                    Status,
                    CreatedAt,
                    UpdatedAt
                FROM QuoteEnquiries
                WHERE QuoteEnquiryId = @Id;

                SELECT
                    QuoteEnquiryItemId,
                    QuoteEnquiryId,
                    ProductId,
                    ProductName,
                    Quantity,
                    Unit,
                    CustomerRequirement,
                    CreatedAt
                FROM QuoteEnquiryItems
                WHERE QuoteEnquiryId = @Id
                ORDER BY QuoteEnquiryItemId;";

            using var multi = await _provider.Connection.QueryMultipleAsync(
                sql,
                new { Id = id });

            var enquiry = await multi.ReadSingleOrDefaultAsync<QuoteEnquiryResponse>();

            if (enquiry is null)
            {
                return null;
            }

            enquiry.Items = (await multi.ReadAsync<QuoteEnquiryItemResponse>()).ToList();

            return enquiry;
        }
    }
}