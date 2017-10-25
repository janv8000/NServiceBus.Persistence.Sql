using System.Data.Common;
using System.IO;
using System.Text;
using Npgsql;
using NUnit.Framework;

public static class PostgreSqlHelper
{
    public static void ExplainCommand(DbCommand command, NpgsqlConnection connection)
    {
        var npgsqlCommand = (NpgsqlCommand)command;
        using (var explainCommand = new NpgsqlCommand($"EXPLAIN ANALYZE {command.CommandText}", connection))
        {
            foreach (NpgsqlParameter par in npgsqlCommand.Parameters)
            {
                explainCommand.Parameters.AddWithValue(par.ParameterName, par.NpgsqlDbType, par.Value);
            }

            var stringBuilder = new StringBuilder();
            using (var reader = explainCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        stringBuilder.AppendLine(reader.GetString(0));
                    }
                }
            }
            var message = stringBuilder.ToString();
            var testContext = TestContext.CurrentContext.Test;
            var path = $@"C:\Code\temp\{testContext.ClassName}_{testContext.Name}.txt";
            File.Delete(path);
            File.WriteAllText(path, message);
        }
    }
}