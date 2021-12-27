using Dapper;
using Discount.Api.Entities.V1;
using Npgsql;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionString = this._configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(this.connectionString);
            var sql = @"INSERT INTO Coupon (ProductId, Description, Amount) 
                        VALUES (@ProductId, @Description, @Amount)";

            var affected = await connection.ExecuteAsync(sql, new { ProductId = coupon.ProductId, Description = coupon.Description });

            return false;
        }

        public Task<bool> DeleteDiscount(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Coupon?> GetDiscount(int productId)
        {
            using var connection = new NpgsqlConnection(this.connectionString);
            const string sql = "SELECT * FROM Coupon WHERE ProductId =  @ProductId";

            var coupon = await connection
                .QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductId = productId });

            return coupon;
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
