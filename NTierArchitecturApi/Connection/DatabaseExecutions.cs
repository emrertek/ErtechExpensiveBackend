using DataAccessLayer.DTOs;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Connection
{
    public class DatabaseExecutions : IDatabaseExecutions
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DatabaseExecutions(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        public int ExecuteDeleteQuery(string storedProcedureName, ParameterList parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName,sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }

                    sqlConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected;

                }
            }
        }


        public string   ExecuteQuery(string storedProcedureName, ParameterList parameters)
        {

            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Bağlantı dizesi (_connectionString) null veya boş!", nameof(_connectionString));
            }

            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }

                    sqlConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())   
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                row[columnName] = value;
                            }
                            results.Add(row);

                        }
                    }

                }
            }

            string jsonResult = JsonConvert.SerializeObject(results);
            return jsonResult;

        }

        public int ExecuteQueryWithOutput(string storedProcedureName, ParameterList parameters, string outputParameterName)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentException("Bağlantı dizesi null veya boş!", nameof(_connectionString));

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Name, parameter.Value ?? DBNull.Value);
                    }

                    // Output parametresini ekliyoruz
                    SqlParameter outputParam = new SqlParameter(outputParameterName, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputParam.Value;
                }
            }
        }

        public string ExecuteQueryWithOutputString(string storedProcedureName, ParameterList parameters, string outputParameterName)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentException("Bağlantı dizesi null veya boş!", nameof(_connectionString));

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(storedProcedureName, sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value ?? DBNull.Value);
                }

                SqlParameter outputParam = new SqlParameter(outputParameterName, SqlDbType.NVarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                sqlConnection.Open();
                command.ExecuteNonQuery();

                return outputParam.Value?.ToString();
            }
        }


        public List<T> ExecuteReader<T>(string storedProcedureName, ParameterList parameters)
        {
            var results = new List<Dictionary<string, object>>();

            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcedureName, sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.Value ?? DBNull.Value);
                }

                sqlConnection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            object? value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            row[columnName] = value;
                        }

                        results.Add(row);
                    }
                }
            }

            string json = JsonConvert.SerializeObject(results);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }



    }
}
