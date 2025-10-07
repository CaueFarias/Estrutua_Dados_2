using Projeto_Listas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Listas.Models
{
    public class Livro
    {
        public int Isbn { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;

        public List<Exemplar> Exemplares { get; } = new();

        public Livro(int isbn, string titulo, string autor, string editora)
        {
            Isbn = isbn;
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
        }

        public void AdicionarExemplar(Exemplar exemplar)
        {
            Exemplares.Add(exemplar);
        }

        public int QtdeExemplares() => Exemplares.Count;

        public int QtdeDisponiveis() => Exemplares.Count(e => e.Disponivel());

        public int QtdeEmprestimos() => Exemplares.Sum(e => e.QtdeEmprestimos());

        public double PercDisponibilidade()
        {
            if (QtdeExemplares() == 0) return 0;
            return (QtdeDisponiveis() / (double)QtdeExemplares()) * 100;
        }
    }
}

