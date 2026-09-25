using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SunriseElectricals.RepositoryService.Database
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly IConfiguration _configuration;
        private IDbConnection? _connection;

        public DbConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var connectionString =
                        _configuration.GetConnectionString("DefaultConnection");

                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new InvalidOperationException(
                            "Database connection string 'DefaultConnection' was not found.");
                    }

                    _connection = new SqlConnection(connectionString);
                }

                return _connection;
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}
