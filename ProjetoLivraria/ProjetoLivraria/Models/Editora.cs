using System.Collections.Generic;

namespace ProjetoLivraria.Models
{
    public class Editora
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }

    }
    public class  EditoraCollection : List<Editora>
    {
        
    }
}