using Projeto_Listas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Listas.Models
{
    public class Livros
    {
        public List<Livro> Acervo { get; } = new();

        public void Adicionar(Livro livro)
        {
            if (!Acervo.Any(l => l.Isbn == livro.Isbn))
                Acervo.Add(livro);
        }

        public Livro? Pesquisar(int isbn)
        {
            return Acervo.FirstOrDefault(l => l.Isbn == isbn);
        }
    }
}

