using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Agenda.Models
{
    public class Contato
    {
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public Data DtNasc { get; set; }

        // Lista de telefones - só leitura fora da classe
        public List<Telefone> Telefones { get; private set; }

        // Construtor
        public Contato(string nome, string email, Data dtNasc)
        {
            Nome = nome;
            Email = email;
            DtNasc = dtNasc;
            Telefones = new List<Telefone>();
        }

        // Retorna idade do contato
        public int GetIdade()
        {
            var nascimento = new DateTime(DtNasc.Ano, DtNasc.Mes, DtNasc.Dia);
            int idade = DateTime.Now.Year - nascimento.Year;

            if (DateTime.Now.Month < nascimento.Month ||
                (DateTime.Now.Month == nascimento.Month && DateTime.Now.Day < nascimento.Day))
            {
                idade--;
            }

            return idade;
        }

        // Adicionar telefone
        public void AdicionarTelefone(Telefone t)
        {
            if (t == null) return;

            if (t.Principal)
            {
                // se o novo for principal, desmarca os outros
                foreach (var tel in Telefones)
                    tel.Principal = false;
            }

            Telefones.Add(t);
        }

        // Retorna telefone principal
        public string GetTelefonePrincipal()
        {
            var tel = Telefones.FirstOrDefault(t => t.Principal);
            return tel != null ? tel.Numero : "Nenhum telefone principal cadastrado.";
        }

        // Exibir todos os dados do contato
        public override string ToString()
        {
            string telefones = Telefones.Count > 0
                ? string.Join(", ", Telefones.Select(t => $"{t.Tipo}: {t.Numero}{(t.Principal ? " (Principal)" : "")}"))
                : "Nenhum telefone cadastrado";

            return $"Nome: {Nome}\nEmail: {Email}\nData Nasc.: {DtNasc}\nIdade: {GetIdade()} anos\nTelefones: {telefones}";
        }

        // Comparar contatos pelo email (único identificador)
        public override bool Equals(object? obj)
        {
            return obj is Contato outro &&
                   Email.Equals(outro.Email, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Email?.ToLower().GetHashCode() ?? 0;
    }
}
