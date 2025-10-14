using Listas_GerenciamentoProj.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Listas_GerenciamentoProj.Forms
{
    public class AddProjectForm : Form
    {
        private TextBox txtId, txtNome;
        private Button btnSalvar, btnCancelar;
        public Projeto ProjetoCriado { get; private set; } = null!;

        public AddProjectForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Adicionar Projeto";
            this.Size = new Size(360, 200);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblId = new Label() { Text = "ID:", Location = new Point(12, 20), AutoSize = true };
            txtId = new TextBox() { Location = new Point(80, 18), Width = 240 };

            var lblNome = new Label() { Text = "Nome:", Location = new Point(12, 60), AutoSize = true };
            txtNome = new TextBox() { Location = new Point(80, 58), Width = 240 };

            btnSalvar = new Button() { Text = "Salvar", Location = new Point(80, 110), Width = 100 };
            btnCancelar = new Button() { Text = "Cancelar", Location = new Point(220, 110), Width = 100 };

            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { lblId, txtId, lblNome, txtNome, btnSalvar, btnCancelar });
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("ID inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nome = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Nome obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ProjetoCriado = new Projeto { Id = id, Nome = nome };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
