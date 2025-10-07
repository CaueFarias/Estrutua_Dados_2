
using Projeto_Listas.Models;

namespace Projeto_Listas
{
    public partial class MenuForm : Form
    {
        private Livros _acervo = new();

        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnAddLivro_Click(object sender, EventArgs e)
        {
            var form = new AdicionarLivroForm(_acervo);
            form.ShowDialog();
        }

        private void btnPesquisarLivro_Click(object sender, EventArgs e)
        {
            var form = new PesquisarLivroForm(_acervo);
            form.ShowDialog();
        }

        private void btnAddExemplar_Click(object sender, EventArgs e)
        {
            var form = new AdicionarExemplarForm(_acervo);
            form.ShowDialog();
        }

        private void btnEmprestimo_Click(object sender, EventArgs e)
        {
            var form = new EmprestimoForm(_acervo);
            form.ShowDialog();
        }

        private void btnDevolucao_Click(object sender, EventArgs e)
        {
            var form = new DevolucaoForm(_acervo);
            form.ShowDialog();
        }
    }
}

