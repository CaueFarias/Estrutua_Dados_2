using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Ambiente> Ambientes { get; set; }

    public Usuario()
    {
        Ambientes = new List<Ambiente>();
    }

    public Usuario(int id, string nome)
    {
        Id = id;
        Nome = nome;
        Ambientes = new List<Ambiente>();
    }

    public bool ConcederPermissao(Ambiente ambiente)
    {
        if (Ambientes.Any(a => a.Id == ambiente.Id))
            return false;

        Ambientes.Add(ambiente);
        return true;
    }

    public bool RevogarPermissao(Ambiente ambiente)
    {
        var encontrado = Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
        if (encontrado == null)
            return false;

        Ambientes.Remove(encontrado);
        return true;
    }

    public override string ToString()
    {
        return $"{Id} - {Nome} (Permissões: {Ambientes.Count})";
    }
}

