using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Veiculo
{
    public int Id { get; set; }
    public int Capacidade { get; set; }
    public int PassageirosTransportados { get; set; }

    public Veiculo(int id, int capacidade)
    {
        Id = id;
        Capacidade = capacidade;
        PassageirosTransportados = 0;
    }

    public void RegistrarPassageiros(int quantidade)
    {
        PassageirosTransportados += quantidade;
    }

    public override string ToString()
    {
        return $"Veículo {Id} - Capacidade: {Capacidade}";
    }
}

