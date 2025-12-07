using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ControleViagens
{
    private List<Viagem> viagens = new();

    public void Registrar(Viagem v) => viagens.Add(v);

    public int Quantidade(Garagem origem, Garagem destino)
    {
        return viagens.Count(v => v.Origem == origem && v.Destino == destino);
    }

    public List<Viagem> Listar(Garagem origem, Garagem destino)
    {
        return viagens
            .Where(v => v.Origem == origem && v.Destino == destino)
            .ToList();
    }

    public int PassageirosTransportados(Garagem origem, Garagem destino)
    {
        return viagens
            .Where(v => v.Origem == origem && v.Destino == destino)
            .Sum(v => v.Passageiros);
    }
}
