using System;
using System.Data.Common;
using Npgsql;
using NServiceBus.Persistence.Sql.ScriptBuilder;
using NUnit.Framework;

[TestFixture]
public class PostgreSqlOutboxPersisterTests : OutboxPersisterTests
{
    public PostgreSqlOutboxPersisterTests() : base(BuildSqlDialect.PostgreSql, null)
    {
        OutboxPersister.PreExecute = command =>
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
    protected override string BuildOperationsFromMessageIdCommand(string messageId)
    {
        return $@"
select ""Operations""
from ""{GetTablePrefix()}{GetTableSuffix()}""
where ""MessageId"" = '{messageId}'";
    }
}