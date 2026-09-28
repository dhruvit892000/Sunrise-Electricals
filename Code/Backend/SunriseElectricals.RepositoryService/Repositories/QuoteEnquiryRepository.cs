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

        public async Task<QuoteEnquiry?> GetByIdAsync(int id)
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

            var enquiry = await multi.ReadSingleOrDefaultAsync<QuoteEnquiry>();

            if (enquiry is null)
            {
                return null;
            }

            _ = await multi.ReadAsync<QuoteEnquiryItem>();

            return enquiry;
        }
    }
}