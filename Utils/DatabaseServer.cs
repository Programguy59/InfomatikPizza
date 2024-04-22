using InfomatikPizza.Tables;
using informatikPizza.Tables.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace informatikPizza.Util;

public static class DatabaseServer
{
	private static SqlConnection? _connection;
	public static void Initialize(int attempts)
    {
        try
        {
            Console.WriteLine("Connecting to the database... (" + ++attempts + ")");
        }
        catch (SqlException e)
        {
            Console.WriteLine("Failed to connect to the database!");
            Console.WriteLine("Cause: " + e.Message);

            Console.WriteLine("Press any key to retry...");
            Console.WriteLine();
            Initialize(attempts);
        }
    }

	private static SqlConnection GetConnection()
    {
     string Host = "sql.itcn.dk";
     int Port = 1433;

     string Database = "magn8244.SKOLE";
     string User = "magn8244.SKOLE";
     string Password = "72Xh87JnCn";

    SqlConnectionStringBuilder sb = new()
        {
            DataSource = Host,
            InitialCatalog = Database,
            UserID = User,
            Password = Password
        };

        var connectionString = sb.ToString();

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        return _connection;
    }

	public static SqlDataReader ExecuteQuery(string query)
    {
        var connection = GetConnection();
        var command = new SqlCommand(query, connection);

        try
        {
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            connection.Dispose();
            command.Dispose();

            throw;
        }
    }
	private static bool ExecuteNonQuery(string query)
    {
        using var connection = GetConnection();
        using var command = new SqlCommand(query, connection);

        return command.ExecuteNonQuery() > 0;
    }


    public static void FetchAddress()
    {
        var query = "EXEC FetchAddress";

        using var reader = ExecuteQuery(query);

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var fullAddress = reader.GetString(1);

            Address address = new(id, fullAddress);
            Database.Address.Add(address);
        }

        reader.Close();
    }

   
    public static void InsertAddress(Address address)
    {
        var query =
            "EXEC CreateBus '" + address.FullAddress;

        var reader = ExecuteQuery(query);
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var fullAddress = reader.GetString(1);

            address.Id = id;
            address.FullAddress = fullAddress;
            // Update the local cache.
            Database.Address.Add(address);
        }

        reader.Close();
    }


    public static void UpdateAddress(int Id, string fullAddress)
    {
        var query =
            "EXEC UpdateBalance '" + Id
                                   + "', " + fullAddress;

        var reader = ExecuteNonQuery(query);
    }

}
