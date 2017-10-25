using System;
using System.Data.Common;
using Npgsql;
using NServiceBus.Persistence.Sql.ScriptBuilder;

public class PostgreSqlSagaPersisterTests : SagaPersisterTests
{
    public PostgreSqlSagaPersisterTests() : base(BuildSqlDialect.PostgreSql, null)
    {
        SagaPersister.PreExecute = command =>
        {
            using (var connection = (NpgsqlConnection)GetConnection()())
            {
                PostgreSqlHelper.ExplainCommand(command, connection);
            }
        };
    }

    protected override Func<DbConnection> GetConnection()
    {
        return () =>
        {
            var connection = PostgreSqlConnectionBuilder.Build();
            connection.Open();
            return connection;
        };
    }
    protected override bool IsConcurrencyException(Exception innerException)
    {
        return innerException.Message.Contains("duplicate key value violates unique constraint");
    }

}