
using Projeto_Listas.Models;

namespace Projeto_Listas
{
    public partial class AdicionarLivroForm : Form
    {
        private Livros _acervo;

        public AdicionarLivroForm(Livros acervo)
        {
            InitializeComponent();
            _acervo = acervo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            int isbn = int.Parse(txtIsbn.Text);
            string titulo = txtTitulo.Text;
            string autor = txtAutor.Text;
            string editora = txtEditora.Text;

            var livro = new Livro(isbn, titulo, autor, editora);
            _acervo.Adicionar(livro);

            MessageBox.Show("Livro adicionado com sucesso!");
            this.Close();
        }
    }
}
