using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;

namespace ProjetoLivraria.View
{
    public partial class frmUsuarioCadastro : Form
    {
        UsuarioController usuarioController = new UsuarioController();
        int acaoTela; 
        Usuario usuarioSelecionado;
        public frmUsuarioCadastro(int acao, Usuario usuario)
        {
            InitializeComponent();
            acaoTela = acao;
            usuarioSelecionado = usuario;
            CarregarTela();
        }

        void CarregarTela()
        {
            if (acaoTela == 0) 
            { 
                this.Text = "Cadastrar Usuário"; 
            }
            else 
            { 
                CarregarDados(); if (acaoTela == 1) 
                { 
                    this.Text = "Alterar Usuário"; 
                }
                else 
                { 
                    this.Text = "Visualizar Usuário"; 
                    DesabilitarCampos(); 
                } 
            }
        }
        void CarregarDados() { if (usuarioSelecionado != null) 
            { 
                txtId.Text = usuarioSelecionado.Id.ToString(); 
                txtNome.Text = usuarioSelecionado.Nome; 
                txtCPF.Text = usuarioSelecionado.CPF; 
                txtEmail.Text = usuarioSelecionado.Email; 
                txtTelefone.Text = usuarioSelecionado.Telefone; 
                txtCargo.Text = usuarioSelecionado.Cargo; 
                txtSenha.Text = usuarioSelecionado.Senha; 
            } 
        }
        void DesabilitarCampos() 
        { 
            txtNome.ReadOnly = true; 
            txtCPF.ReadOnly = true; 
            txtEmail.ReadOnly = true; 
            txtTelefone.ReadOnly = true; 
            txtCargo.ReadOnly = true; 
            txtSenha.ReadOnly = true; 
            btnSalvar.Visible = false; 
            btnCancelar.Visible = false; 
        }
        void Salvar()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                MessageBox.Show("Por favor, preencha os campos Nome e CPF.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Usuario usuario = new Usuario();
            usuario.Nome = txtNome.Text; 
            usuario.CPF = txtCPF.Text; 
            usuario.Email = txtEmail.Text; 
            usuario.Telefone = txtTelefone.Text; 
            usuario.Cargo = txtCargo.Text; 
            usuario.Senha = txtSenha.Text;
            int retorno = 0;

            if (acaoTela == 0)
            {
                retorno = usuarioController.Inserir(usuario);
            }
            else
            {
                usuario.Id = int.Parse(txtId.Text); 
                retorno = usuarioController.Alterar(usuario);
            }

            if(retorno > 0)
            {
                MessageBox.Show("Usuário salvo com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information); 
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Falha ao salvar o registro.", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Cancelar()
        {
            if (MessageBox.Show("Deseja cancelar a operação?", "Confirmação", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question, 
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