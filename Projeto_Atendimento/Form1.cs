using Projeto_Atendimento.Models;
using System;
using System.Windows.Forms;

namespace Projeto_Atendimento
{
    public partial class Form1 : Form
    {
        private Senhas senhas;
        private Guiches guiches;

        public Form1()
        {
            InitializeComponent();
            senhas = new Senhas();
            guiches = new Guiches();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            senhas.gerar();
            MessageBox.Show("Senha gerada com sucesso!");
        }

        private void btnListarSenhas_Click(object sender, EventArgs e)
        {
            listBoxSenhas.Items.Clear();
            foreach (var s in senhas.FilaSenhas)
                listBoxSenhas.Items.Add(s.dadosParciais());
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            guiches.adicionar(new Guiche(guiches.ListaGuiches.Count + 1));
            lblQtdeGuiches.Text = guiches.ListaGuiches.Count.ToString();
        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGuiche.Text, out int idGuiche))
            {
                var guiche = guiches.ListaGuiches.Find(g => g.Id == idGuiche);
                if (guiche != null)
                {
                    if (guiche.chamar(senhas.FilaSenhas))
                        MessageBox.Show($"Guichê {idGuiche} chamou uma senha.");
                    else
                        MessageBox.Show("Nenhuma senha disponível.");
                }
                else
                    MessageBox.Show("Guichê não encontrado.");
            }
        }

        private void btnListarAtendimentos_Click(object sender, EventArgs e)
        {
            listBoxAtendimentos.Items.Clear();
            if (int.TryParse(txtGuiche.Text, out int idGuiche))
            {
                var guiche = guiches.ListaGuiches.Find(g => g.Id == idGuiche);
                if (guiche != null)
                {
                    foreach (var s in guiche.Atendimentos)
                        listBoxAtendimentos.Items.Add(s.dadosCompletos());
                }
                else
                    MessageBox.Show("Guichê não encontrado.");
            }
        }
    }
}
