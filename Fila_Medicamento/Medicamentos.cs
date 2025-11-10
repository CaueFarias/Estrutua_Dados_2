using Fila_Medicamento;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fila_Medicamento
{
    public class Medicamentos
    {
        private List<Medicamento> listaMedicamentos;

        public Medicamentos()
        {
            listaMedicamentos = new List<Medicamento>();
        }

        public void Adicionar(Medicamento medicamento)
        {
            if (!listaMedicamentos.Any(m => m.Equals(medicamento)))
                listaMedicamentos.Add(medicamento);
        }

        public bool Deletar(Medicamento medicamento)
        {
            var med = Pesquisar(medicamento);
            if (med != null && med.QtdeDisponivel() == 0)
            {
                listaMedicamentos.Remove(med);
                return true;
            }
            return false;
        }

        public Medicamento? Pesquisar(Medicamento medicamento)
        {
            return listaMedicamentos.FirstOrDefault(m => m.Id == medicamento.Id);
        }

        public List<Medicamento> Listar() => listaMedicamentos;
    }
}

