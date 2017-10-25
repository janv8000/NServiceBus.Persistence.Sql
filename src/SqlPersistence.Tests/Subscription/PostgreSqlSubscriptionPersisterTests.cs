using System;
using System.Data.Common;
using Npgsql;
using NServiceBus.Persistence.Sql.ScriptBuilder;
using NUnit.Framework;

[TestFixture]
public class PostgreSqlSubscriptionPersisterTests : SubscriptionPersisterTests
{
    public PostgreSqlSubscriptionPersisterTests() : base(BuildSqlDialect.PostgreSql, null)
    {
        SubscriptionPersister.PreExecute = command =>
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