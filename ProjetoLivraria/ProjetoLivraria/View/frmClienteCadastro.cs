using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;

namespace ProjetoLivraria.Views
{
    public partial class frmClienteCadastro : Form
    {
        ClienteControllers clienteController = new ClienteControllers();
        int acaoTela;
        Cliente clienteSelecionado;

        public frmClienteCadastro(int acao, Cliente cliente)
        {
            InitializeComponent(); acaoTela = acao; clienteSelecionado = cliente; CarregarTela();
        }

        void CarregarTela()
        {
            if (acaoTela == 0)
            {
                this.Text = "Cadastrar Cliente";
            }
            else
            {
                CarregarDados(); if (acaoTela == 1)
                {
                    this.Text = "Alterar Cliente";
                }
                else
                {
                    this.Text = "Visualizar Cliente";
                    DesabilitarCampos();
                }
            }
        }

        void CarregarDados()
        {
            if (clienteSelecionado != null)
            {
                txtId.Text = clienteSelecionado.Id.ToString();
                txtNome.Text = clienteSelecionado.Nome;
                txtCPF.Text = clienteSelecionado.CPF;
                txtEmail.Text = clienteSelecionado.Email;
                txtTelefone.Text = clienteSelecionado.Telefone;
                txtEndereco.Text = clienteSelecionado.Endereco;
                txtCEP.Text = clienteSelecionado.CEP;
            }
        }

        void DesabilitarCampos()
        {
            txtNome.ReadOnly = true;
            txtCPF.ReadOnly = true;
            txtEmail.ReadOnly = true; txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            txtCEP.ReadOnly = true; btnSalvar.Visible = false;
            btnCancelar.Visible = false;
        }

        void Salvar()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                MessageBox.Show("Por favor, preencha os campos Nome e CPF.", "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente cliente = new Cliente(); 
            cliente.Nome = txtNome.Text; 
            cliente.CPF = txtCPF.Text; 
            cliente.Email = txtEmail.Text; 
            cliente.Telefone = txtTelefone.Text; 
            cliente.Endereco = txtEndereco.Text; 
            cliente.CEP = txtCEP.Text;

            int retorno = 0;
            if (acaoTela == 0) 
            { 
                retorno = clienteController.Inserir(cliente); 
            }
            else 
            { 
                cliente.Id = int.Parse(txtId.Text); 
                retorno = clienteController.Alterar(cliente); 
            }
            if (retorno > 0) 
                { 
                MessageBox.Show("Cliente salvo com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information); 
                DialogResult = DialogResult.OK; 
            }
            else 
            { 
                MessageBox.Show("Falha ao salvar o registro.", "Erro", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error); 
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