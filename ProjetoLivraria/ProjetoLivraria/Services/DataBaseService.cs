using System.Data;
using System.Data.SqlClient;

namespace ProjetoLivraria.Services
{
    internal class DataBaseService
    {
        private SqlConnection GetConnection()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
                "Data Source=.\\SQLEXPRESS;" +
                "Initial Catalog=ProjetoLivraria;" +
                "Integrated Security=SSPI;";
            connection.Open();
            return connection;
        }

        public int ExecuteSql(SqlCommand command)
        {
            command.Connection = GetConnection();
            return command.ExecuteNonQuery();
        }

        public DataTable GetDataTable(SqlCommand command)
        {

            command.Connection = GetConnection();
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
