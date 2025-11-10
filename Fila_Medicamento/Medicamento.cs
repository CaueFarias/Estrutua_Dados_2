using Fila_Medicamento;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fila_Medicamento
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Laboratorio { get; set; }
        public Queue<Lote> Lotes { get; private set; }

        public Medicamento()
        {
            Lotes = new Queue<Lote>();
        }

        public Medicamento(int id, string nome, string laboratorio)
        {
            Id = id;
            Nome = nome;
            Laboratorio = laboratorio;
            Lotes = new Queue<Lote>();
        }

        public int QtdeDisponivel()
        {
            return Lotes.Sum(l => l.Qtde);
        }

        public void Comprar(Lote lote)
        {
            Lotes.Enqueue(lote);
        }

        public bool Vender(int qtde)
        {
            int totalDisponivel = QtdeDisponivel();
            if (totalDisponivel < qtde)
                return false;

            while (qtde > 0 && Lotes.Count > 0)
            {
                var primeiroLote = Lotes.Peek();
                if (primeiroLote.Qtde <= qtde)
                {
                    qtde -= primeiroLote.Qtde;
                    Lotes.Dequeue();
                }
                else
                {
                    primeiroLote.Qtde -= qtde;
                    qtde = 0;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return $"{Id} - {Nome} - {Laboratorio} - {QtdeDisponivel()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Medicamento outro)
                return this.Id == outro.Id;
            return false;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}

