using System.Text.Json;
using Dapper;
using SunriseElectricals.Core.DTOs;
using SunriseElectricals.RepositoryService.Database;
using SunriseElectricals.RepositoryService.Interfaces;

namespace SunriseElectricals.RepositoryService.Repositories
{
    public class QuoteReplyRepository : IQuoteReplyRepository
    {
        private readonly IDbConnectionProvider _provider;

        public QuoteReplyRepository(IDbConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<QuoteReplyResponse> CreateAsync(CreateQuoteReplyRequest request)
        {
            const string sql = @"
                SET XACT_ABORT ON;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    IF NOT EXISTS
                    (
                        SELECT 1
                        FROM QuoteEnquiries
                        WHERE QuoteEnquiryId = @QuoteEnquiryId
                    )
                    BEGIN
                        THROW 50001, 'Quote enquiry was not found.', 1;
                    END;

                    INSERT INTO QuoteReplies
                    (
                        QuoteEnquiryId,
                        ReplyMessage,
                        RepliedBy,
                        SubTotal,
                        DiscountAmount,
                        TaxAmount,
                        GrandTotal,
                        ValidUntil,
                        CreatedAt
                    )
                    VALUES
                    (
                        @QuoteEnquiryId,
                        @ReplyMessage,
                        @RepliedBy,
                        @SubTotal,
                        @DiscountAmount,
                        @TaxAmount,
                        @GrandTotal,
                        @ValidUntil,
                        SYSUTCDATETIME()
                    );

                    DECLARE @QuoteReplyId INT = CONVERT(INT, SCOPE_IDENTITY());

                    INSERT INTO QuoteReplyItems
                    (
                        QuoteReplyId,
                        QuoteEnquiryItemId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        UnitPrice,
                        DiscountPercent,
                        TaxPercent,
                        LineTotal,
                        CreatedAt
                    )
                    SELECT
                        @QuoteReplyId,
                        QuoteEnquiryItemId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        UnitPrice,
                        DiscountPercent,
                        TaxPercent,
                        LineTotal,
                        SYSUTCDATETIME()
                    FROM OPENJSON(@ItemsJson)
                    WITH
                    (
                        QuoteEnquiryItemId INT '$.QuoteEnquiryItemId',
                        ProductId INT '$.ProductId',
                        ProductName NVARCHAR(250) '$.ProductName',
                        Quantity DECIMAL(18,2) '$.Quantity',
                        Unit NVARCHAR(50) '$.Unit',
                        UnitPrice DECIMAL(18,2) '$.UnitPrice',
                        DiscountPercent DECIMAL(5,2) '$.DiscountPercent',
                        TaxPercent DECIMAL(5,2) '$.TaxPercent',
                        LineTotal DECIMAL(18,2) '$.LineTotal'
                    );

                    UPDATE QuoteEnquiries
                    SET
                        Status = 'Quoted',
                        UpdatedAt = SYSUTCDATETIME()
                    WHERE QuoteEnquiryId = @QuoteEnquiryId;

                    COMMIT TRANSACTION;

                    SELECT
                        QuoteReplyId,
                        QuoteEnquiryId,
                        ReplyMessage,
                        RepliedBy,
                        SubTotal,
                        DiscountAmount,
                        TaxAmount,
                        GrandTotal,
                        ValidUntil,
                        CreatedAt
                    FROM QuoteReplies
                    WHERE QuoteReplyId = @QuoteReplyId;

                    SELECT
                        QuoteReplyItemId,
                        QuoteReplyId,
                        QuoteEnquiryItemId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        UnitPrice,
                        DiscountPercent,
                        TaxPercent,
                        LineTotal,
                        CreatedAt
                    FROM QuoteReplyItems
                    WHERE QuoteReplyId = @QuoteReplyId
                    ORDER BY QuoteReplyItemId;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK TRANSACTION;

                    THROW;
                END CATCH;";

            using var multi = await _provider.Connection.QueryMultipleAsync(
                sql,
                new
                {
                    request.QuoteEnquiryId,
                    request.ReplyMessage,
                    request.RepliedBy,
                    request.SubTotal,
                    request.DiscountAmount,
                    request.TaxAmount,
                    request.GrandTotal,
                    request.ValidUntil,
                    ItemsJson = JsonSerializer.Serialize(request.Items)
                });

            var reply = await multi.ReadSingleAsync<QuoteReplyResponse>();
            reply.Items = (await multi.ReadAsync<QuoteReplyItemResponse>()).ToList();

            return reply;
        }

        public async Task<QuoteReplyResponse?> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT
                    QuoteReplyId,
                    QuoteEnquiryId,
                    ReplyMessage,
                    RepliedBy,
                    SubTotal,
                    DiscountAmount,
                    TaxAmount,
                    GrandTotal,
                    ValidUntil,
                    CreatedAt
                FROM QuoteReplies
                WHERE QuoteReplyId = @Id;

                SELECT
                    QuoteReplyItemId,
                    QuoteReplyId,
                    QuoteEnquiryItemId,
                    ProductId,
                    ProductName,
                    Quantity,
                    Unit,
                    UnitPrice,
                    DiscountPercent,
                    TaxPercent,
                    LineTotal,
                    CreatedAt
                FROM QuoteReplyItems
                WHERE QuoteReplyId = @Id
                ORDER BY QuoteReplyItemId;";

            using var multi = await _provider.Connection.QueryMultipleAsync(
                sql,
                new { Id = id });

            var reply = await multi.ReadSingleOrDefaultAsync<QuoteReplyResponse>();

            if (reply is null)
            {
                return null;
            }

            reply.Items = (await multi.ReadAsync<QuoteReplyItemResponse>()).ToList();

            return reply;
        }

        public async Task<QuoteReplyResponse?> GetByEnquiryIdAsync(int quoteEnquiryId)
        {
            const string sql = @"
                SELECT TOP 1
                    QuoteReplyId,
                    QuoteEnquiryId,
                    ReplyMessage,
                    RepliedBy,
                    SubTotal,
                    DiscountAmount,
                    TaxAmount,
                    GrandTotal,
                    ValidUntil,
                    CreatedAt
                FROM QuoteReplies
                WHERE QuoteEnquiryId = @QuoteEnquiryId
                ORDER BY QuoteReplyId DESC;

                SELECT
                    QuoteReplyItemId,
                    QuoteReplyId,
                    QuoteEnquiryItemId,
                    ProductId,
                    ProductName,
                    Quantity,
                    Unit,
                    UnitPrice,
                    DiscountPercent,
                    TaxPercent,
                    LineTotal,
                    CreatedAt
                FROM QuoteReplyItems
                WHERE QuoteReplyId =
                (
                    SELECT TOP 1 QuoteReplyId
                    FROM QuoteReplies
                    WHERE QuoteEnquiryId = @QuoteEnquiryId
                    ORDER BY QuoteReplyId DESC
                )
                ORDER BY QuoteReplyItemId;";

            using var multi = await _provider.Connection.QueryMultipleAsync(
                sql,
                new { QuoteEnquiryId = quoteEnquiryId });

            var reply = await multi.ReadSingleOrDefaultAsync<QuoteReplyResponse>();

            if (reply is null)
            {
                return null;
            }

            reply.Items = (await multi.ReadAsync<QuoteReplyItemResponse>()).ToList();

            return reply;
        }

        public async Task<QuoteReplyResponse?> UpdateAsync(
            int id,
            UpdateQuoteReplyRequest request)
        {
            const string sql = @"
                SET XACT_ABORT ON;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    IF NOT EXISTS
                    (
                        SELECT 1
                        FROM QuoteReplies
                        WHERE QuoteReplyId = @Id
                    )
                    BEGIN
                        ROLLBACK TRANSACTION;

                        SELECT CAST(0 AS BIT) AS Found;

                        RETURN;
                    END;

                    UPDATE QuoteReplies
                    SET
                        ReplyMessage = @ReplyMessage,
                        RepliedBy = @RepliedBy,
                        SubTotal = @SubTotal,
                        DiscountAmount = @DiscountAmount,
                        TaxAmount = @TaxAmount,
                        GrandTotal = @GrandTotal,
                        ValidUntil = @ValidUntil
                    WHERE QuoteReplyId = @Id;

                    DELETE FROM QuoteReplyItems
                    WHERE QuoteReplyId = @Id;

                    INSERT INTO QuoteReplyItems
                    (
                        QuoteReplyId,
                        QuoteEnquiryItemId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        UnitPrice,
                        DiscountPercent,
                        TaxPercent,
                        LineTotal,
                        CreatedAt
                    )
                    SELECT
                        @Id,
                        QuoteEnquiryItemId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        UnitPrice,
                        DiscountPercent,
                        TaxPercent,
                        LineTotal,
                        SYSUTCDATETIME()
                    FROM OPENJSON(@ItemsJson)
                    WITH
                    (
                        QuoteEnquiryItemId INT '$.QuoteEnquiryItemId',
                        ProductId INT '$.ProductId',
                        ProductName NVARCHAR(250) '$.ProductName',
                        Quantity DECIMAL(18,2) '$.Quantity',
                        Unit NVARCHAR(50) '$.Unit',
                        UnitPrice DECIMAL(18,2) '$.UnitPrice',
                        DiscountPercent DECIMAL(5,2) '$.DiscountPercent',
                        TaxPercent DECIMAL(5,2) '$.TaxPercent',
                        LineTotal DECIMAL(18,2) '$.LineTotal'
                    );

                    UPDATE QuoteEnquiries
                    SET
                        Status = 'Quoted',
                        UpdatedAt = SYSUTCDATETIME()
                    WHERE QuoteEnquiryId =
                    (
                        SELECT QuoteEnquiryId
                        FROM QuoteReplies
                        WHERE QuoteReplyId = @Id
                    );

                    COMMIT TRANSACTION;

                    SELECT CAST(1 AS BIT) AS Found;

                    SELECT
                        QuoteReplyId,
                        QuoteEnquiryId,
                        ReplyMessage,
                        RepliedBy,
                        SubTotal,
                        DiscountAmount,
                        TaxAmount,
                        GrandTotal,
                        ValidUntil,
                        CreatedAt
                    FROM QuoteReplies
                    WHERE QuoteReplyId = @Id;

                    SELECT
                        QuoteReplyItemId,
                        QuoteReplyId,
                        QuoteEnquiryItemId,
                        ProductId,
                        ProductName,
                        Quantity,
                        Unit,
                        UnitPrice,
                        DiscountPercent,
                        TaxPercent,
                        LineTotal,
                        CreatedAt
                    FROM QuoteReplyItems
                    WHERE QuoteReplyId = @Id
                    ORDER BY QuoteReplyItemId;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK TRANSACTION;

                    THROW;
                END CATCH;";

            using var multi = await _provider.Connection.QueryMultipleAsync(
                sql,
                new
                {
                    Id = id,
                    request.ReplyMessage,
                    request.RepliedBy,
                    request.SubTotal,
                    request.DiscountAmount,
                    request.TaxAmount,
                    request.GrandTotal,
                    request.ValidUntil,
                    ItemsJson = JsonSerializer.Serialize(request.Items)
                });

            var found = await multi.ReadSingleAsync<bool>();

            if (!found)
            {
                return null;
            }

            var reply = await multi.ReadSingleAsync<QuoteReplyResponse>();
            reply.Items = (await multi.ReadAsync<QuoteReplyItemResponse>()).ToList();

            return reply;
        }
    }
}
