using ProjetoLivraria.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoLivraria.View
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }
        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarioSelecao frm = new frmUsuarioSelecao();
            frm.ShowDialog();
        }
        private void clientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmClienteSelecao frm = new frmClienteSelecao();
            frm.ShowDialog();
        }

        private void livrosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmLivroSelecao frm = new frmLivroSelecao();
            frm.ShowDialog();
        }

        private void editoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditoraSelecao frm = new frmEditoraSelecao();
            frm.ShowDialog();
        }
    }
}
