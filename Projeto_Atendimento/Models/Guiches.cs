using Projeto_Atendimento.Models;
using System.Collections.Generic;

namespace Projeto_Atendimento.Models
{
    public class Guiches
    {
        public List<Guiche> ListaGuiches { get; private set; }

        public Guiches()
        {
            ListaGuiches = new List<Guiche>();
        }

        public void adicionar(Guiche guiche)
        {
            ListaGuiches.Add(guiche);
        }
    }
}
