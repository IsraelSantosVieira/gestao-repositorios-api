using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;
namespace RepositorioApp.Api._Config.HealthChecks
{
    // Sample SQL Connection Health Check
    public class NpgSqlConnectionHealthCheck : IHealthCheck
    {
        private static readonly string DefaultTestQuery = "select 1";

        public NpgSqlConnectionHealthCheck(string connectionString)
            : this(connectionString, DefaultTestQuery)
        {
        }

        public NpgSqlConnectionHealthCheck(string connectionString, string testQuery)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            TestQuery = testQuery;
        }

        public string ConnectionString { get; }

        public string TestQuery { get; }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery != null)
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;
                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}
