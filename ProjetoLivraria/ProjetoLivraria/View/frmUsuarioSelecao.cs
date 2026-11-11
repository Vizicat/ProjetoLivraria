using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;


namespace ProjetoLivraria.View
{
    public partial class frmUsuarioSelecao : Form
    {
        UsuarioController usuarioController = new UsuarioController();
        public frmUsuarioSelecao()
        {
            InitializeComponent();

            Pesquisar();
        }

        void Pesquisar()
        {
            dgvRegistros.DataSource = null;

            if (!string.IsNullOrEmpty(txtId.Text))
            {
                Usuario retorno = usuarioController.GetById(int.Parse(txtId.Text));
                UsuarioCollection listaUsuarios = new UsuarioCollection();

                if (retorno != null)
                {
                    listaUsuarios.Add(retorno);
                }

                dgvRegistros.DataSource = listaUsuarios;
            }
            else if (!string.IsNullOrEmpty(txtNome.Text))
            {
                dgvRegistros.DataSource = usuarioController.GetByNome(txtNome.Text);
            }
            else
            {
                dgvRegistros.DataSource = usuarioController.GetAll();
            }
        }
        Usuario GetUsuarioGrade()
        {
            if (dgvRegistros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum registro selecionado.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
            {
                return dgvRegistros.SelectedRows[0].DataBoundItem as Usuario;
            }
        }
        void Excluir()
        {
            Usuario usuario = GetUsuarioGrade();

            if (usuario == null) return;

            if (MessageBox.Show("Deseja realmente excluir este usuário?", "Confirmação", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (usuarioController.Excluir(usuario.Id) > 0)
                {
                    MessageBox.Show("Usuário excluído com sucesso.", "Sucesso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Pesquisar();
                }
                else
                {
                    MessageBox.Show("Não foi possível excluir o registro.", "Erro", 
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
            Usuario usuario = GetUsuarioGrade();

            if (usuario == null) return;

            frmUsuarioCadastro frm = new frmUsuarioCadastro(1, usuario);
            if (frm.ShowDialog() == DialogResult.OK) 
            { 
                Pesquisar(); 
            }
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Usuario usuario = GetUsuarioGrade(); 
            if (usuario == null) return;

            frmUsuarioCadastro frm = new frmUsuarioCadastro(2, usuario); 
            frm.ShowDialog();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            frmUsuarioCadastro frm = new frmUsuarioCadastro(0, null); 
            if (frm.ShowDialog() == DialogResult.OK) 
            { 
                Pesquisar(); 
            }
        }
    }
}