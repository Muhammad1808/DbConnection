using Npgsql;
using System.Data;

namespace DbConnection;

public class Table
{
    public static HashSet<string> validDataTypes = new HashSet<string>
        {
            "serial", "int", "integer", "bigint", "smallint", "varchar", "char", "text", "boolean",
            "date", "timestamp", "real", "double precision", "numeric", "bytea"
        };

    public static void AddTable(string tableName, Dictionary<string, string> columns)
    {
        
        try
        {
            using (var connection = new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                foreach(var column in columns)
                {
                    if(!validDataTypes.Contains(column.Value.ToLower()))
                    {
                        throw new Exception($"Invalid data type. Please use a valid PostgreSQL data type");
                    }
                }
                var columnsDefinition = string.Join(", ", columns.Select(col => $"{col.Key} {col.Value}"));
                string query = $@"create table if not exists {tableName}(
                            id serial primary key,
                            {columnsDefinition}
                        )";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Table '{tableName}' created successfully.");
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
        }
    }

    public static void UpdateTable(string tableName, string newTableName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                string query = $"ALTER TABLE \"{tableName}\" RENAME TO \"{newTableName}\"";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Table '{tableName}' successfully updated.");
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void DeleteTable(string tableName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                string query = $@"DROP TABLE  {tableName}";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Table '{tableName}' has been deleted.");
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void ListTables()
    {
        using (var connection = new NpgsqlConnection(Program.connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                var item = connection.GetSchema("Tables");
                foreach (DataRow table in item.Rows)
                {
                    Console.WriteLine(table["Table_name"]);
                }
            }
            connection.Close();
        }
    }
}
