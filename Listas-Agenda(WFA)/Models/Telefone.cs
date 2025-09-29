using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_Agenda_WFA_.Models
{
    public class Telefone
    {
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public bool Principal { get; set; }

        public Telefone(string tipo, string numero, bool principal = false)
        {
            Tipo = tipo;
            Numero = numero;
            Principal = principal;
        }

        public override string ToString()
        {
            return $"{Tipo}: {Numero}" + (Principal ? " (Principal)" : "");
        }
    }
}

