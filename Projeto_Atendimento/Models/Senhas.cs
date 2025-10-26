using Projeto_Atendimento.Models;
using System.Collections.Generic;

namespace Projeto_Atendimento.Models
{
    public class Senhas
    {
        public int ProximoAtendimento { get; private set; }
        public Queue<Senha> FilaSenhas { get; private set; }

        public Senhas()
        {
            ProximoAtendimento = 1;
            FilaSenhas = new Queue<Senha>();
        }

        public void gerar()
        {
            FilaSenhas.Enqueue(new Senha(ProximoAtendimento++));
        }
    }
}
