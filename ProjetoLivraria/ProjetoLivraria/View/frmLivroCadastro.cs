using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;

namespace ProjetoLivraria.View
{
    public partial class frmLivroCadastro : Form
    {
        LivroController livroController = new LivroController(); 
        int acaoTela; 
        Livro livroSelecionado;
        public frmLivroCadastro(int acao, Livro livro)
        {
            InitializeComponent();
            acaoTela = acao;
            livroSelecionado = livro;
            CarregarTela();
        }

        void CarregarTela()
        {
            if (acaoTela == 0)
            {
                this.Text = "Cadastrar Livro";
            }
            else
            {
                CarregarDados();
                if (acaoTela == 1)
                {
                    this.Text = "Alterar Livro";
                }
                else
                {
                    this.Text = "Visualizar Livro";
                    DesabilitarCampos();
                }
            }
        }

        void CarregarDados()
        {
            if (livroSelecionado != null) 
            { 
                txtId.Text = livroSelecionado.Id.ToString(); 
                txtTitulo.Text = livroSelecionado.Titulo; 
                txtCodDeBarras.Text = livroSelecionado.CodDeBarras; 
                txtAutor.Text = livroSelecionado.Autor; 
                txtCategoria.Text = livroSelecionado.Categoria; 
                txtPreco.Text = livroSelecionado.Preco.ToString("F2"); 
                txtEstoque.Text = livroSelecionado.Estoque.ToString(); 
            }
        }

        void DesabilitarCampos()
        {
            txtTitulo.ReadOnly = true; 
            txtCodDeBarras.ReadOnly = true; 
            txtAutor.ReadOnly = true; 
            txtCategoria.ReadOnly = true; 
            txtPreco.ReadOnly = true; 
            txtEstoque.ReadOnly = true; 
            btnSalvar.Visible = false; 
            btnCancelar.Visible = false;
        }

        void Salvar()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text)) 
            { 
                MessageBox.Show("Por favor, preencha o Título do livro.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco)) 
            { 
                MessageBox.Show("Informe um Preço válido.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            if (!int.TryParse(txtEstoque.Text, out int estoque)) 
            { 
                MessageBox.Show("Informe uma quantidade de Estoque válida.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            Livro livro = new Livro(); 
            livro.Titulo = txtTitulo.Text; 
            livro.CodDeBarras = txtCodDeBarras.Text; 
            livro.Autor = txtAutor.Text; 
            livro.Categoria = txtCategoria.Text; 
            livro.Preco = preco; 
            livro.Estoque = estoque;

            int retorno = 0;

            if (acaoTela == 0) 
            { 
                retorno = livroController.Inserir(livro); 
            }
            else 
            { 
                livro.Id = int.Parse(txtId.Text);
                retorno = livroController.Alterar(livro); 
            }
            if (retorno > 0) 
            { 
                MessageBox.Show("Livro salvo com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information); 
                DialogResult = DialogResult.OK; 
            }
            else 
            { 
                MessageBox.Show("Falha ao salvar o livro.", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        void Cancelar()
        {
            if (MessageBox.Show("Deseja cancelar a operação?", "Confirmação", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2) == DialogResult.Yes) 
            { 
                DialogResult = DialogResult.Cancel; 
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
    }
}
