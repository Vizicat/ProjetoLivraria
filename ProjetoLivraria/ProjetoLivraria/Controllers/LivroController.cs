using System.Data;
using System.Data.SqlClient;
using ProjetoLivraria.Models;
using ProjetoLivraria.Services;

namespace ProjetoLivraria.Controllers
{
    public class LivroController
    {
        DataBaseService _database = new DataBaseService();
        public int Inserir(Livro livro)
        {
            string query = "INSERT INTO Livro (Titulo, CodDeBarras, Autor, Categoria, Preco, Estoque) " +
                "VALUES (@Titulo, @CodDeBarras, @Autor, @Categoria, @Preco, @Estoque)";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Titulo", livro.Titulo);
            command.Parameters.AddWithValue("@CodDeBarras", livro.CodDeBarras);
            command.Parameters.AddWithValue("@Autor", livro.Autor);
            command.Parameters.AddWithValue("@Categoria", livro.Categoria);
            command.Parameters.AddWithValue("@Preco", livro.Preco);
            command.Parameters.AddWithValue("@Estoque", livro.Estoque);

            return _database.ExecuteSql(command);   
        }

        public int Alterar(Livro livro)
        {
            string query = "UPDATE Livro SET " +
                "Titulo = @Titulo, " +
                "CodDeBarras = @CodDeBarras, " +
                "Autor = @Autor, " +
                "Categoria = @Categoria, " +
                "Preco = @Preco, " 
                + "Estoque = @Estoque " +
                "WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Titulo", livro.Titulo);
            command.Parameters.AddWithValue("@CodDeBarras", livro.CodDeBarras);
            command.Parameters.AddWithValue("@Autor", livro.Autor);
            command.Parameters.AddWithValue("@Categoria", livro.Categoria);
            command.Parameters.AddWithValue("@Preco", livro.Preco);
            command.Parameters.AddWithValue("@Estoque", livro.Estoque);
            command.Parameters.AddWithValue("@Id", livro.Id);

            return _database.ExecuteSql(command);
        }

        public int Excluir(int id)
        {
            string query = "DELETE FROM Livro WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            return _database.ExecuteSql(command);
        }

        public Livro GetById(int id)
        {
            string query = "SELECT * FROM Livro WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Id", id);
            DataTable dataTable = _database.GetDataTable(command);
            if (dataTable.Rows.Count > 0)
            {
                Livro livro = new Livro();
                livro.Id = (int)dataTable.Rows[0]["Id"];
                livro.Titulo = (string)dataTable.Rows[0]["Titulo"];
                livro.CodDeBarras = (string)dataTable.Rows[0]["CodDeBarras"];
                livro.Autor = (string)dataTable.Rows[0]["Autor"];
                livro.Categoria = (string)dataTable.Rows[0]["Categoria"];
                livro.Preco = (decimal)dataTable.Rows[0]["Preco"];
                livro.Estoque = (int)dataTable.Rows[0]["Estoque"];

                return livro;
            }
            else
            {
                return null;
            }
        }
        private LivroCollection GetByFilters(string filtro = "")
        {
            string query = "SELECT * FROM Livro";
            if (filtro != "")
            {
                query += " WHERE " + filtro;
            }

            query += " ORDER BY Titulo";
            SqlCommand command = new SqlCommand(query);
            DataTable dataTable = _database.GetDataTable(command);
            LivroCollection livros = new LivroCollection();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Livro livro = new Livro();
                livro.Id = (int)dataTable.Rows[i]["Id"];
                livro.Titulo = (string)dataTable.Rows[i]["Titulo"];
                livro.CodDeBarras = (string)dataTable.Rows[i]["CodDeBarras"];
                livro.Autor = (string)dataTable.Rows[i]["Autor"];
                livro.Categoria = (string)dataTable.Rows[i]["Categoria"];
                livro.Preco = (decimal)dataTable.Rows[i]["Preco"];
                livro.Estoque = (int)dataTable.Rows[i]["Estoque"];
                livros.Add(livro);
            }

            return livros;
        }

        public LivroCollection GetAll()
        {
            return GetByFilters();
        }

        public LivroCollection GetByTitulo(string titulo)
        {
            return GetByFilters("Titulo LIKE '%" + titulo + "%'");
        }
    }
}