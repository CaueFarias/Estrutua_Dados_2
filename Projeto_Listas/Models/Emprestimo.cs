using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Listas.Models
{
    public class Emprestimo
    {
        public DateTime DtEmprestimo { get; set; }
        public DateTime? DtDevolucao { get; set; }

        public Emprestimo()
        {
            DtEmprestimo = DateTime.Now;
            DtDevolucao = null;
        }

        public void Devolver()
        {
            DtDevolucao = DateTime.Now;
        }

        public bool EstaDevolvido => DtDevolucao.HasValue;
    }
}
