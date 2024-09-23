using Npgsql;
using System.Data;
namespace DbConnection;

public class Column
{
    public static HashSet<string> validDataTypes = new HashSet<string>
        {
            "serial", "int", "integer", "bigint", "smallint", "varchar", "char", "text", "boolean",
            "date", "timestamp", "real", "double precision", "numeric", "bytea"
        };

    public static void AddColumn(string tableName, string columnName, string columnType)
    {    
        try
        {
            using (var connection = new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                if (!validDataTypes.Contains(columnType.ToLower()))
                {
                    throw new Exception($"Invalid data type. Please use a valid PostgreSQL data type");
                }
                string query = $@"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType}";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Column '{columnName}' of type '{columnType}' added to table '{tableName}'.");
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void UpdateColumn(string tableName, string columnName, string newColumnName,string newDataType)
    {
        try
        {
            using (var connection=new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                if (!validDataTypes.Contains(newDataType.ToLower()))
                {
                    throw new Exception($"Invalid data type. Please use a valid PostgreSQL data type");
                }
                string query=$@"ALTER TABLE {tableName} RENAME COLUMN {columnName} to {newColumnName}";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Column '{columnName}' successfully updated.");
                }
                string query1=$@"ALTER TABLE {tableName} ALTER COLUMN {newColumnName} TYPE {newDataType}";

                using (var command= new NpgsqlCommand(query1, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Column '{columnName}' data type successfully updated.");
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }

    public static void DeleteColumn(string tableName, string columnName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                string query = $"ALTER TABLE {tableName} DROP COLUMN {columnName}";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Column {columnName} successfully deleted");
                }
                connection.Close();
            }
        }
        catch( Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static List<string> ListColumn(string tableName)
    {
        List<string> columnNames = new List<string>();
        try
        {
            using (var connection = new NpgsqlConnection(Program.connectionString))
            {
                connection.Open();
                string query = $"SELECT column_name FROM information_schema.columns WHERE table_name = '{tableName}'";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return columnNames;
    }
}
