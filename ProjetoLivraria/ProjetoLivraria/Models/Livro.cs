using System.Collections.Generic;

namespace ProjetoLivraria.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string CodDeBarras { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }
    public class LivroCollection : List<Livro>
    {
        
    }
}
