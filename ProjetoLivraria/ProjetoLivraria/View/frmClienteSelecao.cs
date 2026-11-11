using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;

namespace ProjetoLivraria.Views
{
    public partial class frmClienteSelecao : Form
    {
        ClienteControllers clienteController = new ClienteControllers();

        public frmClienteSelecao()
        {
            InitializeComponent();

            Pesquisar();
        }

        void Pesquisar()
        {
            dgvRegistros.DataSource = null;

            if (!string.IsNullOrEmpty(txtId.Text))
            {
                Cliente retorno = clienteController.GetById(int.Parse(txtId.Text));
                ClienteCollection listaClientes = new ClienteCollection();

                if (retorno != null)
                {
                    listaClientes.Add(retorno);
                }

                dgvRegistros.DataSource = listaClientes;
            }

            else if (!string.IsNullOrEmpty(txtNome.Text))
            {
                dgvRegistros.DataSource = clienteController.GetByNome(txtNome.Text);
            }

            else
            {
                dgvRegistros.DataSource = clienteController.GetAll();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            frmClienteCadastro frm = new frmClienteCadastro(0, null);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Pesquisar();
            }
        }
        Cliente GetClienteGrade()
        {
            if (dgvRegistros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum registro selecionado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            else
            {
                return dgvRegistros.SelectedRows[0].DataBoundItem as Cliente;
            }
        }

        void Excluir()
        {
            Cliente cliente = GetClienteGrade();

            if (cliente == null) return;

            if (MessageBox.Show("Deseja realmente excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (clienteController.Excluir(cliente.Id) > 0)
                {
                    MessageBox.Show("Cliente excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Pesquisar();
                }
                else
                {
                    MessageBox.Show("Não foi possível excluir o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            Excluir();
        }
        private void btnAlterar_Click_1(object sender, EventArgs e)
        {
            Cliente cliente = GetClienteGrade();

            if (cliente == null) return;

            frmClienteCadastro frm = new frmClienteCadastro(1, cliente);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Pesquisar();
            }
        }
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Cliente cliente = GetClienteGrade();

            if (cliente == null) return;

            frmClienteCadastro frm = new frmClienteCadastro(2, cliente);
            frm.ShowDialog();
        }
    }
}