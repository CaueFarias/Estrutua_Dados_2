using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_GerenciamentoProj.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        /// <summary>1=alta,2=media,3=baixa</summary>
        public int Prioridade { get; set; } = 2;
        /// <summary>"Aberta","Fechada","Cancelada"</summary>
        public string Status { get; set; } = "Aberta";
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataConclusao { get; set; }

        public void Concluir()
        {
            if (Status == "Aberta")
            {
                Status = "Fechada";
                DataConclusao = DateTime.Now;
            }
        }

        public void Cancelar()
        {
            if (Status == "Aberta")
            {
                Status = "Cancelada";
                DataConclusao = DateTime.Now;
            }
        }

        public void Reabrir()
        {
            if (Status != "Aberta")
            {
                Status = "Aberta";
                DataConclusao = null;
            }
        }

        public override string ToString() => $"[{Id}] {Titulo} ({Status})";
    }
}

