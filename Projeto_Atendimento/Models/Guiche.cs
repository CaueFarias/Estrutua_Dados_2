using Projeto_Atendimento.Models;
using System;
using System.Collections.Generic;

namespace Projeto_Atendimento.Models
{
    public class Guiche
    {
        public int Id { get; set; }
        public Queue<Senha> Atendimentos { get; private set; }

        public Guiche()
        {
            Id = 0;
            Atendimentos = new Queue<Senha>();
        }

        public Guiche(int id)
        {
            Id = id;
            Atendimentos = new Queue<Senha>();
        }

        public bool chamar(Queue<Senha> filaSenhas)
        {
            if (filaSenhas.Count == 0)
                return false;

            Senha s = filaSenhas.Dequeue();
            s.DataAtend = DateTime.Now;
            s.HoraAtend = DateTime.Now;
            Atendimentos.Enqueue(s);
            return true;
        }
    }
}
