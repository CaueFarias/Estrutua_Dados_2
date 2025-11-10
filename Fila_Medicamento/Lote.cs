using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fila_Medicamento
{
    public class Lote
    {
        public int Id { get; set; }
        public int Qtde { get; set; }
        public DateTime Venc { get; set; }

        public Lote() { }

        public Lote(int id, int qtde, DateTime venc)
        {
            Id = id;
            Qtde = qtde;
            Venc = venc;
        }

        public override string ToString()
        {
            return $"{Id} - {Qtde} - {Venc:dd/MM/yyyy}";
        }
    }
}

