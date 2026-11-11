using System.Data;
using System.Data.SqlClient;
using ProjetoLivraria.Models;
using ProjetoLivraria.Services;

namespace ProjetoLivraria.Controllers
{
    public class EditoraController
    {
        DataBaseService _database = new DataBaseService();

        public int Inserir(Editora editora)
        {
            string query = "INSERT INTO Editora (NomeFantasia, RazaoSocial, CNPJ, Telefone, Email, Cidade)" +
                "VALUES(@NomeFantasia, @RazaoSocial, @CNPJ, @Telefone, @Email, @Cidade)";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@NomeFantasia", editora.NomeFantasia);
            command.Parameters.AddWithValue("@RazaoSocial", editora.RazaoSocial);
            command.Parameters.AddWithValue("@CNPJ", editora.CNPJ);
            command.Parameters.AddWithValue("@Telefone", editora.Telefone);
            command.Parameters.AddWithValue("@Email", editora.Email);
            command.Parameters.AddWithValue("@Cidade", editora.Cidade);
            return _database.ExecuteSql(command);
        }

        public int Alterar(Editora editora)
        {
            string query = "UPDATE Editora SET " +
                "NomeFantasia = @NomeFantasia, " +
                "RazaoSocial = @RazaoSocial, " +
                "CNPJ = @CNPJ, " +
                "Telefone = @Telefone, " +
                "Email = @Email, " +
                "Cidade = @Cidade " +
                "WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@NomeFantasia", editora.NomeFantasia);
            command.Parameters.AddWithValue("@RazaoSocial", editora.RazaoSocial);
            command.Parameters.AddWithValue("@CNPJ", editora.CNPJ);
            command.Parameters.AddWithValue("@Telefone", editora.Telefone);
            command.Parameters.AddWithValue("@Email", editora.Email);
            command.Parameters.AddWithValue("@Cidade", editora.Cidade);
            command.Parameters.AddWithValue("@Id", editora.Id);
            return _database.ExecuteSql(command);
        }

        public int Excluir(int id)
        {
            string query = "DELETE FROM Editora WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            return _database.ExecuteSql(command);
        }

        public Editora GetById(int id)
        {
            string query = "SELECT * FROM Editora WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            DataTable dataTable = _database.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                Editora editora = new Editora();
                editora.Id = (int)dataTable.Rows[0]["Id"];
                editora.NomeFantasia = (string)dataTable.Rows[0]["NomeFantasia"].ToString();
                editora.RazaoSocial = (string)dataTable.Rows[0]["RazaoSocial"].ToString();
                editora.CNPJ = (string)dataTable.Rows[0]["CNPJ"].ToString();
                editora.Telefone = (string)dataTable.Rows[0]["Telefone"].ToString();
                editora.Email = (string)dataTable.Rows[0]["Email"].ToString();
                editora.Cidade = (string)dataTable.Rows[0]["Cidade"].ToString();
                return editora;
            }

            else
            {
                return null;
            }
        }

        private EditoraCollection GetByFilters(string filtro = "")
        {
            string query = "SELECT * FROM Editora";

            if (filtro != "")
            {
                query += " WHERE " + filtro;
            }

            query += " ORDER BY NomeFantasia";
            SqlCommand command = new SqlCommand(query);
            DataTable dataTable = _database.GetDataTable(command);
            EditoraCollection editoras = new EditoraCollection();
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Editora editora = new Editora();
                editora.Id = (int)dataTable.Rows[i]["Id"];
                editora.NomeFantasia = (string)dataTable.Rows[i]["NomeFantasia"];
                editora.RazaoSocial = (string)dataTable.Rows[i]["RazaoSocial"];
                editora.CNPJ = (string)dataTable.Rows[i]["CNPJ"];
                editora.Telefone = (string)dataTable.Rows[i]["Telefone"];
                editora.Email = (string)dataTable.Rows[i]["Email"];
                editora.Cidade = (string)dataTable.Rows[i]["Cidade"];
                editoras.Add(editora);
            }
            return editoras;
        }

        public EditoraCollection GetAll()
        {
            return GetByFilters();
        }

        public EditoraCollection GetByNome(string nome)
        {
            return GetByFilters("NomeFantasia LIKE '%" + nome + "%'");
        }
    }
}