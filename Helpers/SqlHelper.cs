using System.Data;
using System.Data.SqlClient;

namespace UsersBlogs.Helpers;

public class SqlHelper
{
    const string _connectionString = @"Server=DESKTOP-5G1J9JL\MSSQLSERVER01;DataBase=UsersBlogs;Trusted_Connection=true";
    public static DataTable GetDatas(string query)
    {
        using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        sqlConnection.Open();
        DataTable dt = new DataTable();
        using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
        sqlDataAdapter.Fill(dt);
        sqlConnection.Close();
        return dt;
    }

    public static int Exec(string query)
    {
        using SqlConnection sqlConnection = new SqlConnection(_connectionString);
        sqlConnection.Open();
        DataTable dt = new DataTable();
        using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
        int result = sqlCommand.ExecuteNonQuery();
        sqlConnection.Close();
        return result;

    }
}
