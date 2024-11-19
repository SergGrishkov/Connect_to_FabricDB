using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
 
namespace DatabaseScriptExecutor
{
    class DbConnect
    {
        static void Main(string[] args)
        {
																		 
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Error: ConnectionString or SQL-script not found.");
                Environment.Exit(1);
            }
 
            string connectionString = args[0];
			string sqlQuery = args[1];
 
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
 
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var resultTable = new DataTable();
							resultTable.Load(reader);
 
							var jsonResult = ConvertDataTableToJson(resultTable);
							Console.WriteLine(jsonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }
		
		static string ConvertDataTableToJson(DataTable table)
		{
			var rows = new List<Dictionary<string, object?>>();
	 
			foreach (DataRow row in table.Rows)
			{
				var rowDict = new Dictionary<string, object?>();
	 
				foreach (DataColumn column in table.Columns)
				{
					rowDict[column.ColumnName] = row[column] == DBNull.Value ? null : row[column];
				}
	 
				rows.Add(rowDict);
			}
	 
			var options = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				WriteIndented = true
			};
			return JsonSerializer.Serialize(rows, options);
		}
    }
}