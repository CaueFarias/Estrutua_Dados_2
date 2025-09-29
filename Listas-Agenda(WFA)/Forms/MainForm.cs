using Listas_Agenda_WFA_.Models;

namespace Listas_Agenda_WFA_.Forms

    {
        public partial class MainForm : Form
        {
            private Contatos contatos = new();

            public MainForm()
            {
                InitializeComponent();
            }

            private void btnAdicionar_Click(object sender, EventArgs e)
            {
                var frm = new AddContatoForm();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (contatos.Adicionar(frm.ContatoCriado))
                        MessageBox.Show("Contato adicionado!");
                    else
                        MessageBox.Show("Já existe um contato com esse email.");
                }
            }

            private void btnListar_Click(object sender, EventArgs e)
            {
                lstContatos.Items.Clear();
                foreach (var c in contatos.Agenda)
                    lstContatos.Items.Add(c);
            }
        }
    }

