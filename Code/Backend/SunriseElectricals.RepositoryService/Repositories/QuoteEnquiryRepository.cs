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
            const string insertEnquirySql = @"
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
                OUTPUT
                    INSERTED.QuoteEnquiryId,
                    INSERTED.QuoteNumber,
                    INSERTED.CustomerName,
                    INSERTED.CompanyName,
                    INSERTED.Email,
                    INSERTED.Phone,
                    INSERTED.Location,
                    INSERTED.Message,
                    INSERTED.Status,
                    INSERTED.CreatedAt,
                    INSERTED.UpdatedAt
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
                );";

            const string insertItemSql = @"
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
                VALUES
                (
                    @QuoteEnquiryId,
                    @ProductId,
                    @ProductName,
                    @Quantity,
                    @Unit,
                    @CustomerRequirement,
                    SYSUTCDATETIME()
                );";

            var connection = _provider.Connection;
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            using var transaction = connection.BeginTransaction();

            try
            {
                var quoteNumber =
                    $"SE-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}"[..20];

                var enquiry = await connection.QuerySingleAsync<QuoteEnquiry>(
                    insertEnquirySql,
                    new
                    {
                        QuoteNumber = quoteNumber,
                        request.CustomerName,
                        request.CompanyName,
                        request.Email,
                        request.Phone,
                        request.Location,
                        request.Message
                    },
                    transaction);

                foreach (var item in request.Items)
                {
                    await connection.ExecuteAsync(
                        insertItemSql,
                        new
                        {
                            QuoteEnquiryId = enquiry.QuoteEnquiryId,
                            item.ProductId,
                            item.ProductName,
                            item.Quantity,
                            item.Unit,
                            item.CustomerRequirement
                        },
                        transaction);
                }

                transaction.Commit();

                return enquiry;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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

            var connection = _provider.Connection;
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }

            using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });

            var enquiry = await multi.ReadSingleOrDefaultAsync<QuoteEnquiry>();

            if (enquiry is null)
            {
                return null;
            }

            // QuoteEnquiry remains the API's primary entity for this step.
            // Items are persisted transactionally and can be exposed by a
            // dedicated read DTO in the next quote workflow step.
            _ = await multi.ReadAsync<QuoteEnquiryItem>();

            return enquiry;
        }
    }
}
