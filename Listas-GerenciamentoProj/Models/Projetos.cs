using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_GerenciamentoProj.Models
{
    public class Projetos
    {
        private readonly List<Projeto> itens = new();
        public IReadOnlyList<Projeto> Itens => itens.AsReadOnly();

        public bool Adicionar(Projeto p)
        {
            if (itens.Any(x => x.Id == p.Id)) return false;
            itens.Add(p);
            return true;
        }

        public bool Remover(Projeto p)
        {
            var existente = itens.FirstOrDefault(x => x.Id == p.Id);
            if (existente == null) return false;
            if (existente.Tarefas.Count > 0) return false; // só remove sem tarefas
            return itens.Remove(existente);
        }

        public Projeto? Buscar(int projetoId)
            => itens.FirstOrDefault(x => x.Id == projetoId);

        public List<Projeto> Listar() => itens.ToList();
    }
}
