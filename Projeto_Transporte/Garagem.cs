using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Garagem
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Stack<Veiculo> PilhaVeiculos { get; private set; }

    public Garagem(int id, string nome)
    {
        Id = id;
        Nome = nome;
        PilhaVeiculos = new Stack<Veiculo>();
    }

    public void Estacionar(Veiculo v)
    {
        PilhaVeiculos.Push(v);
    }

    public Veiculo? Retirar()
    {
        if (PilhaVeiculos.Count == 0)
            return null;
        return PilhaVeiculos.Pop();
    }

    public int QuantidadeVeiculos() => PilhaVeiculos.Count;

    public int PotencialTransporte() =>
        PilhaVeiculos.Sum(v => v.Capacidade);

    public override string ToString()
    {
        return $"{Nome} (ID {Id}) - Veículos: {QuantidadeVeiculos()}";
    }
}

