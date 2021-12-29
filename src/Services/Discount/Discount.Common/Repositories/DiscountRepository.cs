using Dapper;
using Npgsql;

namespace Discount.Common.Repositories;

public class DiscountRepository : IDiscountRepository
{
    //private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public DiscountRepository(string connectionString)
    {
       // this._configuration = configuration;
       // this.connectionString = this._configuration.GetValue<string>("DatabaseSettings:ConnectionString");
       this.connectionString = connectionString;
    }

    public async Task<bool> CreateDiscountAsync(Entities.V1.Discount coupon)
    {
        using var connection = new NpgsqlConnection(this.connectionString);
        var sql = @"INSERT INTO Discount (ProductId, Description, Amount)
                        VALUES (@ProductId, @Description, @Amount)";

        var affectedCount = await connection.ExecuteAsync(
            sql,
            new
            {
                ProductId = coupon.ProductId,
                Description = coupon.Description,
                Amount = coupon.Amount
            });

        // only return false if no rows were effected
        return affectedCount != 0;
    }

    public async Task<bool> DeleteDiscountAsync(string productId)
    {
        using var connection = new NpgsqlConnection(this.connectionString);
        var sql = @"DELETE FROM Discount WHERE CouponId = @Id";

        var affectedCount = await connection.ExecuteAsync(
            sql,
            new
            {
                Id = productId
            });

        // only return false if no rows were effected
        return affectedCount != 0;
    }

    public async Task<Entities.V1.Discount?> GetDiscountAsync(string productId)
    {
        using var connection = new NpgsqlConnection(this.connectionString);
        const string sql = "SELECT * FROM Discount WHERE ProductId =  @ProductId";

        var coupon = await connection
            .QueryFirstOrDefaultAsync<Discount.Common.Entities.V1.Discount>(sql, new { ProductId = productId });

        return coupon;
    }

    public async Task<bool> UpdateDiscountAsync(Entities.V1.Discount coupon)
    {
        using var connection = new NpgsqlConnection(this.connectionString);
        var sql = @"UPDATE Discount
                        SET
                            ProductId = @ProductId
                            , Description = @Description
                            , Amount @Amount
                        WHERE CouponId = @Id";

        var affectedCount = await connection.ExecuteAsync(
            sql,
            new
            {
                ProductId = coupon.ProductId,
                Description = coupon.Description,
                Amount = coupon.Amount,
                Id = coupon.Id
            });

        // only return false if no rows were effected
        return affectedCount != 0;
    }
}