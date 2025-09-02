using ProjetoVendas.Models;
using System;
using System.Windows.Forms;

namespace ProjetoVendas.Forms
{
    public partial class MainForm : Form
    {
        private Vendedores vendedores = new Vendedores();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            new CadastroVendedorForm(vendedores).ShowDialog();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            new ConsultarVendedorForm(vendedores).ShowDialog();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Informe o ID do vendedor:"));
            var v = vendedores.SearchVendedor(id);
            if (v == null) MessageBox.Show("Vendedor não encontrado!");
            else if (!vendedores.DelVendedor(v)) MessageBox.Show("Não é possível excluir: vendedor possui vendas!");
            else MessageBox.Show("Vendedor excluído com sucesso!");
        }

        private void btnRegistrarVenda_Click(object sender, EventArgs e)
        {
            new RegistrarVendaForm(vendedores).ShowDialog();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            new ListarVendedoresForm(vendedores).ShowDialog();
        }
    }
}
