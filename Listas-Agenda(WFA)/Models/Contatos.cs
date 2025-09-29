using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_Agenda_WFA_.Models
{
    public class Contatos
    {
        private readonly List<Contato> agenda = new();

        public IReadOnlyList<Contato> Agenda => agenda.AsReadOnly();

        public bool Adicionar(Contato c)
        {
            if (agenda.Any(a => a.Equals(c))) return false;
            agenda.Add(c);
            return true;
        }

        public Contato? Pesquisar(string email)
        {
            return agenda.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool Alterar(Contato c)
        {
            var existente = Pesquisar(c.Email);
            if (existente == null) return false;

            existente.Nome = c.Nome;
            existente.DtNasc = c.DtNasc;
            existente.Telefones.Clear();
            foreach (var t in c.Telefones) existente.AdicionarTelefone(t);
            return true;
        }

        public bool Remover(string email)
        {
            var existente = Pesquisar(email);
            if (existente == null) return false;
            agenda.Remove(existente);
            return true;
        }
    }
}

