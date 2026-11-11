    using System;
    using System.Windows.Forms;
    using ProjetoLivraria.Controllers;
    using ProjetoLivraria.Models;

    namespace ProjetoLivraria.View
    {
        public partial class frmLivroSelecao : Form
        {
            LivroController livroController = new LivroController();
            public frmLivroSelecao()
            {
                InitializeComponent();
                Pesquisar();
            }

            void Pesquisar()
            {
                dgvRegistros.DataSource = null;
                if (!string.IsNullOrEmpty(txtId.Text))
                {
                    if (int.TryParse(txtId.Text, out int idBusca))
                    {
                        Livro retorno = livroController.GetById(idBusca);
                        LivroCollection listaLivros = new LivroCollection();
                        if (retorno != null)
                        {
                            listaLivros.Add(retorno);
                        }
                        dgvRegistros.DataSource = listaLivros;
                    }
                    else
                    {
                        MessageBox.Show("O código deve ser um número válido.", "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    }
                }
                else if (!string.IsNullOrEmpty(txtTitulo.Text)) 
                { 
                    dgvRegistros.DataSource = livroController.GetByTitulo(txtTitulo.Text); 
                } 
                else 
                { 
                    dgvRegistros.DataSource = livroController.GetAll(); 
                }
            }

            private void btnPesquisar_Click(object sender, EventArgs e)
            {
                Pesquisar();
            }

            private void btnAdicionar_Click(object sender, EventArgs e)
            {
                frmLivroCadastro frm = new frmLivroCadastro(0, null); 
                if (frm.ShowDialog() == DialogResult.OK) 
                { 
                    Pesquisar(); 
                }
            }

            Livro GetLivroGrade()
            {
                if (dgvRegistros.SelectedRows.Count == 0) 
                { 
                    MessageBox.Show("Nenhum livro selecionado.", "Atenção", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning); return null; 
                } 
                else 
                { 
                    return 
                        dgvRegistros.SelectedRows[0].DataBoundItem as Livro; 
                }
            }

            void Excluir()
            {
                Livro livro = GetLivroGrade(); 
                if (livro == null) return;

                if (MessageBox.Show("Deseja realmente excluir este livro?", "Confirmação", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes) 
                { 
                    if (livroController.Excluir(livro.Id) > 0) 
                        { 
                        MessageBox.Show("Livro excluído com sucesso.", "Sucesso", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information); 
                        Pesquisar(); 
                    } 
                    else 
                    { 
                        MessageBox.Show("Não foi possível excluir o livro.", "Erro", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error); 
                    } 
                }
            }

            private void btnExcluir_Click(object sender, EventArgs e)
            {
                Excluir();
            }

            private void btnAlterar_Click(object sender, EventArgs e)
            {
                Livro livro = GetLivroGrade(); 
                if (livro == null) return;

                frmLivroCadastro frm = new frmLivroCadastro(1, livro); 
                if (frm.ShowDialog() == DialogResult.OK) 
                { 
                    Pesquisar(); 
                }
            }

            private void btnVisualizar_Click(object sender, EventArgs e)
            {
                Livro livro = GetLivroGrade(); 
                if (livro == null) return;

                frmLivroCadastro frm = new frmLivroCadastro(2, livro); 
                frm.ShowDialog();
            }
        }
    }
