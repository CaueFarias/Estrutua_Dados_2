using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Viagem
{
    public Garagem Origem { get; }
    public Garagem Destino { get; }
    public Veiculo Veiculo { get; }
    public int Passageiros { get; }
    public DateTime Data { get; }

    public Viagem(Garagem origem, Garagem destino, Veiculo veiculo, int passageiros)
    {
        Origem = origem;
        Destino = destino;
        Veiculo = veiculo;
        Passageiros = passageiros;
        Data = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Data:dd/MM HH:mm} | Veículo {Veiculo.Id} | {Origem.Nome} → {Destino.Nome} | Passageiros: {Passageiros}";
    }
}

