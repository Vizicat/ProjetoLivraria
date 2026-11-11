using System.Data;
using System.Data.SqlClient;
using ProjetoLivraria.Models;
using ProjetoLivraria.Services;

namespace ProjetoLivraria.Controllers
{
    public class ClienteControllers
    {
        DataBaseService _database = new DataBaseService();

        public int Inserir(Cliente cliente)
        {
            string query = "INSERT INTO Cliente (Nome, CPF, Email, Telefone, Endereco, CEP) " +
                "VALUES (@Nome, @CPF, @Email, @Telefone, @Endereco, @CEP)";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@CPF", cliente.CPF);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            command.Parameters.AddWithValue("@CEP", cliente.CEP);

            return _database.ExecuteSql(command);
        }

        public int Alterar(Cliente cliente)
        {
            string query = "UPDATE Cliente SET " +
                "Nome = @Nome, " +
                "CPF = @CPF, " +
                "Email = @Email, " +
                "Telefone = @Telefone, " +
                "Endereco = @Endereco, " +
                "CEP = @CEP " +
                "WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@CPF", cliente.CPF);
            command.Parameters.AddWithValue("@Email", cliente.Email);
            command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            command.Parameters.AddWithValue("@CEP", cliente.CEP);
            command.Parameters.AddWithValue("@Id", cliente.Id);

            return _database.ExecuteSql(command);
        }

        public int Excluir(int id)
        {
            string query = "DELETE FROM Cliente WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            return _database.ExecuteSql(command);
        }

        public Cliente GetById(int id)
        {
            string query = "SELECT * FROM Cliente WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            DataTable dataTable = _database.GetDataTable(command);
            if (dataTable.Rows.Count > 0)
            {
                Cliente cliente = new Cliente();
                cliente.Id = (int)dataTable.Rows[0]["id"];
                cliente.Nome = (string)dataTable.Rows[0]["nome"];
                cliente.CPF = (string)dataTable.Rows[0]["cpf"];
                cliente.Email = (string)dataTable.Rows[0]["email"];
                cliente.Telefone = (string)dataTable.Rows[0]["telefone"];
                cliente.Endereco = (string)dataTable.Rows[0]["endereco"];
                cliente.CEP = (string)dataTable.Rows[0]["cep"];

                return cliente;
            }
            else
            {
                return null;
            }
        }

        private ClienteCollection GetByFilters(string filtro = "")
        {
            string query = "SELECT * FROM Cliente";
            if (filtro != "")
            {
                query += " WHERE " + filtro;
            }
            query += " ORDER BY Nome";
            SqlCommand command = new SqlCommand(query);
            DataTable dataTable = _database.GetDataTable(command);
            ClienteCollection clientes = new ClienteCollection();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Cliente cliente = new Cliente();
                cliente.Id = (int)dataTable.Rows[i]["id"];
                cliente.Nome = (string)dataTable.Rows[i]["nome"];
                cliente.CPF = (string)dataTable.Rows[i]["cpf"];
                cliente.Email = (string)dataTable.Rows[i]["email"];
                cliente.Telefone = (string)dataTable.Rows[i]["telefone"];
                cliente.Endereco = (string)dataTable.Rows[i]["endereco"];
                cliente.CEP = (string)dataTable.Rows[i]["cep"];
                clientes.Add(cliente);
            }

            return clientes;
        }

        public ClienteCollection GetAll()
        {
            return GetByFilters();
        }

        public ClienteCollection GetByNome(string nome)
        {
            return GetByFilters("Nome LIKE '%" + nome + "%'");
        }
    }
}