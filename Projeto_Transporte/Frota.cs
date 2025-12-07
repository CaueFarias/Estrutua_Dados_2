using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Frota
{
    public List<Veiculo> Veiculos { get; private set; }
    public List<Garagem> Garagens { get; private set; }
    public ControleViagens Controle { get; private set; }
    public bool JornadaIniciada { get; private set; }

    public Frota()
    {
        Veiculos = new();
        Garagens = new();
        Controle = new();
        JornadaIniciada = false;
    }

    public bool CadastrarVeiculo(Veiculo v)
    {
        if (JornadaIniciada) return false;
        Veiculos.Add(v);
        return true;
    }

    public bool CadastrarGaragem(Garagem g)
    {
        if (JornadaIniciada) return false;
        Garagens.Add(g);
        return true;
    }

    public bool IniciarJornada()
    {
        if (JornadaIniciada) return false;
        if (Garagens.Count < 2) return false;

        int g = 0;
        foreach (var v in Veiculos)
        {
            Garagens[g].Estacionar(v);
            g = (g + 1) % Garagens.Count;
        }

        JornadaIniciada = true;
        return true;
    }

    public void EncerrarJornada()
    {
        JornadaIniciada = false;
    }

    public bool LiberarViagem(int idOrigem, int idDestino, int passageiros)
    {
        if (!JornadaIniciada) return false;

        var origem = Garagens.FirstOrDefault(g => g.Id == idOrigem);
        var destino = Garagens.FirstOrDefault(g => g.Id == idDestino);

        if (origem == null || destino == null) return false;

        var veiculo = origem.Retirar();
        if (veiculo == null) return false;

        if (passageiros > veiculo.Capacidade) return false;

        veiculo.RegistrarPassageiros(passageiros);
        destino.Estacionar(veiculo);

        Controle.Registrar(new Viagem(origem, destino, veiculo, passageiros));
        return true;
    }
}

