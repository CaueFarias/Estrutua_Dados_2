using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda.Models
{
    public class Telefone
    {
        public string Tipo { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public bool Principal { get; set; }

        public Telefone() { }

        public Telefone(string tipo, string numero, bool principal = false)
        {
            Tipo = tipo;
            Numero = numero;
            Principal = principal;
        }

        public override string ToString()
            => $"{Tipo}: {Numero}" + (Principal ? " (Principal)" : "");
    }
}
