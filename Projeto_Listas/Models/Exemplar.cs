using Projeto_Listas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Listas.Models
    public class Exemplar
    {
        public int Tombo { get; set; }
        public List<Emprestimo> Emprestimos { get; } = new();

        public Exemplar(int tombo)
        {
            Tombo = tombo;
        }

        public bool Disponivel()
        {
            return Emprestimos.Count == 0 || Emprestimos.Last().EstaDevolvido;
        }

        public bool Emprestar()
        {
            if (!Disponivel()) return false;

            Emprestimos.Add(new Emprestimo());
            return true;
        }

        public bool Devolver()
        {
            var emprestimo = Emprestimos.LastOrDefault(e => !e.EstaDevolvido);
            if (emprestimo == null) return false;

            emprestimo.Devolver();
            return true;
        }

        public int QtdeEmprestimos() => Emprestimos.Count;
    }
}

