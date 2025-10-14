using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_GerenciamentoProj.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Tarefa> Tarefas { get; } = new();

        public void AdicionarTarefa(Tarefa t)
        {
            if (!Tarefas.Any(x => x.Id == t.Id))
                Tarefas.Add(t);
        }

        public bool RemoverTarefa(Tarefa t)
        {
            return Tarefas.Remove(Tarefas.FirstOrDefault(x => x.Id == t.Id));
        }

        public Tarefa? BuscarTarefa(int tarefaId)
        {
            return Tarefas.FirstOrDefault(x => x.Id == tarefaId);
        }

        public List<Tarefa> TarefasPorStatus(string s)
            => Tarefas.Where(t => t.Status.Equals(s, System.StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Tarefa> TarefasPorPrioridade(int p)
            => Tarefas.Where(t => t.Prioridade == p).ToList();

        public int TotalAbertas() => Tarefas.Count(t => t.Status == "Aberta");
        public int TotalFechadas() => Tarefas.Count(t => t.Status == "Fechada");
    }
}

