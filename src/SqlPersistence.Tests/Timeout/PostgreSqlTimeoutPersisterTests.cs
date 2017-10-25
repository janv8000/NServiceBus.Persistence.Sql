using System;
using System.Data.Common;
using Npgsql;
using NServiceBus.Persistence.Sql.ScriptBuilder;
using NUnit.Framework;

[TestFixture]
public class PostgreSqlTimeoutPersisterTests : TimeoutPersisterTests
{
    public PostgreSqlTimeoutPersisterTests() : base(BuildSqlDialect.PostgreSql, null)
    {
        TimeoutPersister.PreExecute = command =>
        {
            using (var connection = (NpgsqlConnection)GetConnection()())
            {
                connection.Open();
                PostgreSqlHelper.ExplainCommand(command, connection);
            }
        };
    }

    protected override Func<DbConnection> GetConnection()
    {
        return PostgreSqlConnectionBuilder.Build;
    }
}