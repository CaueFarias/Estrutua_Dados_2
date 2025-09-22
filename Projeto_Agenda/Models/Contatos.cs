using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Agenda.Models
{
    public class Contatos
    {
        private readonly List<Contato> agenda = new List<Contato>();
        public IReadOnlyList<Contato> Agenda => agenda.AsReadOnly();

        // Adicionar novo contato
        public bool Adicionar(Contato c)
        {
            if (agenda.Any(a => a.Equals(c)))
                return false;
            agenda.Add(c);
            return true;
        }

        // Pesquisar contato existente
        public Contato? Pesquisar(Contato c)
        {
            return agenda.FirstOrDefault(a => a.Equals(c));
        }

        // Alterar contato existente
        public bool Alterar(Contato c)
        {
            var existente = Pesquisar(c);
            if (existente == null) return false;

            existente.Nome = c.Nome;
            existente.DtNasc = c.DtNasc;

            // Atualizar lista de telefones corretamente
            existente.Telefones.Clear();
            foreach (var tel in c.Telefones)
            {
                existente.AdicionarTelefone(tel);
            }

            return true;
        }
