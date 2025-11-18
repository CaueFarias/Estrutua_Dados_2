using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Cadastro
{
    public List<Usuario> Usuarios { get; set; }
    public List<Ambiente> Ambientes { get; set; }

    private const string ARQUIVO = "dados.json";

    public Cadastro()
    {
        Usuarios = new List<Usuario>();
        Ambientes = new List<Ambiente>();
    }

    // -------- PERSISTÊNCIA ----------
    public void Upload()
    {
        var pacote = new
        {
            Usuarios,
            Ambientes
        };

        File.WriteAllText(ARQUIVO, JsonSerializer.Serialize(pacote, new JsonSerializerOptions { WriteIndented = true }));
    }

    public void Download()
    {
        if (!File.Exists(ARQUIVO)) return;

        var json = File.ReadAllText(ARQUIVO);

        var pacote = JsonSerializer.Deserialize<Temp>(json);

        if (pacote != null)
        {
            Usuarios = pacote.Usuarios ?? new List<Usuario>();
            Ambientes = pacote.Ambientes ?? new List<Ambiente>();
        }
    }

    private class Temp
    {
        public List<Usuario>? Usuarios { get; set; }
        public List<Ambiente>? Ambientes { get; set; }
    }

    // -------- USUÁRIOS ----------
    public void AdicionarUsuario(Usuario usuario)
    {
        if (!Usuarios.Any(u => u.Id == usuario.Id))
            Usuarios.Add(usuario);
    }

    public Usuario? PesquisarUsuario(Usuario usuario)
    {
        return Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
    }

    public bool RemoverUsuario(Usuario usuario)
    {
        var u = PesquisarUsuario(usuario);
        if (u == null || u.Ambientes.Count > 0) return false;

        return Usuarios.Remove(u);
    }

    // -------- AMBIENTES ----------
    public void AdicionarAmbiente(Ambiente ambiente)
    {
        if (!Ambientes.Any(a => a.Id == ambiente.Id))
            Ambientes.Add(ambiente);
    }

    public Ambiente? PesquisarAmbiente(Ambiente ambiente)
    {
        return Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
    }

    public bool RemoverAmbiente(Ambiente ambiente)
    {
        var a = PesquisarAmbiente(ambiente);
        if (a == null) return false;

        // remover do usuários que tenham permissão
        foreach (var u in Usuarios)
            u.RevogarPermissao(a);

        return Ambientes.Remove(a);
    }
}
