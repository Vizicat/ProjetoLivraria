using System.Data; 
using System.Data.SqlClient; 
using ProjetoLivraria.Models; 
using ProjetoLivraria.Services;

namespace ProjetoLivraria.Controllers
{
    public class UsuarioController
    {
        DataBaseService _database = new DataBaseService();

        public int Inserir(Usuario usuario)
        {
            string query = "INSERT INTO Usuario (Nome, CPF, Email, Telefone, Cargo, Senha) "
                + "VALUES (@Nome, @CPF, @Email, @Telefone, @Cargo, @Senha); ";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@CPF", usuario.CPF);
            command.Parameters.AddWithValue("@Email", usuario.Email);
            command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@Cargo", usuario.Cargo);
            command.Parameters.AddWithValue("@Senha", usuario.Senha);

            return _database.ExecuteSql(command);
        }

        public int Alterar (Usuario usuario)
        {
            string query = "UPDATE Usuario SET " +
                "Nome = @Nome, " +
                "CPF = @CPF, " +
                "Email = @Email, " +
                "Telefone = @Telefone, " +
                "Cargo = @Cargo, " +
                "Senha = @Senha " +
                "WHERE Id = @Id;";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@CPF", usuario.CPF);
            command.Parameters.AddWithValue("@Email", usuario.Email);
            command.Parameters.AddWithValue("@Telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@Cargo", usuario.Cargo);
            command.Parameters.AddWithValue("@Senha", usuario.Senha);
            command.Parameters.AddWithValue("@Id", usuario.Id);

            return _database.ExecuteSql(command);
        }

        public int Excluir(int id)
        {
            string query = "DELETE FROM Usuario WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);

            return _database.ExecuteSql(command);
        }

        public Usuario GetById(int id)
        {
            string query = "SELECT * FROM Usuario WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            DataTable dataTable = _database.GetDataTable(command);
            if (dataTable.Rows.Count > 0)
            {
                Usuario usuario = new Usuario();
                usuario.Id = (int)dataTable.Rows[0]["id"];
                usuario.Nome = (string)dataTable.Rows[0]["nome"];
                usuario.CPF = (string)dataTable.Rows[0]["cpf"];
                usuario.Email = (string)dataTable.Rows[0]["email"];
                usuario.Telefone = (string)dataTable.Rows[0]["telefone"];
                usuario.Cargo = (string)dataTable.Rows[0]["cargo"];
                usuario.Senha = (string)dataTable.Rows[0]["senha"];
                return usuario;
            }
            else
            {
                return null;
            }

        }
        private UsuarioCollection GetByFilters(string filtro = "")
        {
            string query = "SELECT * FROM Usuario";
            if (filtro != "")
            {
                query += " WHERE " + filtro;
            }
            query += " ORDER BY Nome";
            SqlCommand command = new SqlCommand(query);
            DataTable dataTable = _database.GetDataTable(command);
            UsuarioCollection usuarios = new UsuarioCollection();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Usuario usuario = new Usuario();
                usuario.Id = (int)dataTable.Rows[i]["id"];
                usuario.Nome = (string)dataTable.Rows[i]["nome"];
                usuario.CPF = (string)dataTable.Rows[i]["cpf"];
                usuario.Email = (string)dataTable.Rows[i]["email"];
                usuario.Telefone = (string)dataTable.Rows[i]["telefone"];
                usuario.Cargo = (string)dataTable.Rows[i]["cargo"];
                usuario.Senha = (string)dataTable.Rows[i]["senha"];
                usuarios.Add(usuario);
            }
            return usuarios;
        }
        public UsuarioCollection GetAll()
        {
            return GetByFilters();
        }
        public UsuarioCollection GetByNome(string nome)
        {
            return GetByFilters("Nome LIKE '%" + nome + "%'");
        }
    }
}