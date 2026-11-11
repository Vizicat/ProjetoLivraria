using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;

namespace ProjetoLivraria.View
{
    public partial class frmEditoraSelecao : Form
    {
        EditoraController editoraController = new EditoraController();
        public frmEditoraSelecao()
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
                    Editora retorno = editoraController.GetById(idBusca); 
                    EditoraCollection listaEditoras = new EditoraCollection(); 
                    if (retorno != null) { listaEditoras.Add(retorno); 
                    } 
                    dgvRegistros.DataSource = listaEditoras; 
                } 
                else 
                { 
                    MessageBox.Show("O código deve ser um número válido.", "Atenção", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                } 
            } 
            else if (!string.IsNullOrEmpty(txtNome.Text)) 
            { 
                dgvRegistros.DataSource = editoraController.GetByNome(txtNome.Text); 
            } 
            else 
            { 
                dgvRegistros.DataSource = editoraController.GetAll(); 
            } 
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            frmEditoraCadastro frm = new frmEditoraCadastro(0, null); 
            if (frm.ShowDialog() == DialogResult.OK) 
            { 
                Pesquisar(); 
            }
        }

        Editora GetEditoraGrade() 
        { 
            if (dgvRegistros.CurrentRow == null || dgvRegistros.CurrentRow.DataBoundItem == null) 
            { 
                MessageBox.Show("Nenhuma editora selecionada.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); return null; 
            } 
            return 
                dgvRegistros.CurrentRow.DataBoundItem as Editora; 
        }

        void Excluir() 
        { 
            Editora editora = GetEditoraGrade(); 
            if (editora == null) return; 
            if (MessageBox.Show("Deseja realmente excluir esta editora?", 
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2) == DialogResult.Yes) 
            { 
                if (editoraController.Excluir(editora.Id) > 0) 
                { 
                    MessageBox.Show("Editora excluída com sucesso.", "Sucesso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    Pesquisar(); 
                } 
                else 
                { 
                    MessageBox.Show("Não foi possível excluir a editora.", "Erro", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error); 
                } 
            } 
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Editora editora = GetEditoraGrade(); 
            if (editora == null) return; 
            frmEditoraCadastro frm = new frmEditoraCadastro(1, editora); 
            if (frm.ShowDialog() == DialogResult.OK) 
            { 
                Pesquisar(); 
            }
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Editora editora = GetEditoraGrade(); 
            if (editora == null) return; 
            frmEditoraCadastro frm = new frmEditoraCadastro(2, editora); 
            frm.ShowDialog();
        }
    }
}
