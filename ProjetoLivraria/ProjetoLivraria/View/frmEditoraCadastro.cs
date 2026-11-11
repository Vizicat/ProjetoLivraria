using System;
using System.Windows.Forms;
using ProjetoLivraria.Controllers;
using ProjetoLivraria.Models;

namespace ProjetoLivraria.View
{
    public partial class frmEditoraCadastro : Form
    {
        EditoraController editoraController = new EditoraController(); 
        int acaoTela; 
        Editora editoraSelecionada;
        public frmEditoraCadastro(int acao, Editora editora)
        {
            InitializeComponent();
            acaoTela = acao;
            editoraSelecionada = editora;
            CarregarTela();
        }

        void CarregarTela() 
        { 
            if (acaoTela == 0) 
            { 
                this.Text = "Cadastrar Editora"; 
            } 
            else 
            { 
                CarregarDados(); 
                if (acaoTela == 1) 
                { 
                    this.Text = "Alterar Editora"; 
                } 
                else 
                { 
                    this.Text = "Visualizar Editora"; 
                    DesabilitarCampos(); 
                } 
            } 
        }

        void CarregarDados() 
        { 
            if (editoraSelecionada != null) 
            { 
                txtId.Text = editoraSelecionada.Id.ToString(); 
                txtNomeFantasia.Text = editoraSelecionada.NomeFantasia; 
                txtRazaoSocial.Text = editoraSelecionada.RazaoSocial; 
                txtCNPJ.Text = editoraSelecionada.CNPJ; 
                txtTelefone.Text = editoraSelecionada.Telefone; 
                txtEmail.Text = editoraSelecionada.Email; 
                txtCidade.Text = editoraSelecionada.Cidade; 
            } 
        }

        void DesabilitarCampos() 
        { 
            txtNomeFantasia.ReadOnly = true; 
            txtRazaoSocial.ReadOnly = true; 
            txtCNPJ.ReadOnly = true; txtTelefone.ReadOnly = true; 
            txtEmail.ReadOnly = true; txtCidade.ReadOnly = true; 
            btnSalvar.Visible = false; 
            btnCancelar.Visible = false; 
        }

        void Salvar() 
        { 
            if (string.IsNullOrWhiteSpace(txtNomeFantasia.Text)) 
            { 
                MessageBox.Show("Por favor, preencha o Nome Fantasia da editora.", "Atenção", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning); 
                return; 
            } 
            if (string.IsNullOrWhiteSpace(txtCNPJ.Text)) 
            { 
                MessageBox.Show("Por favor, preencha o CNPJ da editora.", "Atenção", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning); return; 
            } Editora editora = new Editora(); 
            editora.NomeFantasia = txtNomeFantasia.Text; 
            editora.RazaoSocial = txtRazaoSocial.Text; 
            editora.CNPJ = txtCNPJ.Text; 
            editora.Telefone = txtTelefone.Text; 
            editora.Email = txtEmail.Text; 
            editora.Cidade = txtCidade.Text; 
            int retorno = 0; if (acaoTela == 0) 
            { 
                retorno = editoraController.Inserir(editora); 
            } 
            else 
            { editora.Id = int.Parse(txtId.Text); 
                retorno = editoraController.Alterar(editora); 
            } 
            if (retorno > 0) 
            { 
                MessageBox.Show("Editora salva com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information); 
                DialogResult = DialogResult.OK; 
            } 
            else 
            { 
                MessageBox.Show("Falha ao salvar a editora.", "Erro", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error); } 
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
