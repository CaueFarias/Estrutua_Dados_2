using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_Agenda_WFA_.Models
{
    public class Data
    {
        public int Dia { get; private set; }
        public int Mes { get; private set; }
        public int Ano { get; private set; }

        public Data() { }
        public Data(int dia, int mes, int ano) => SetData(dia, mes, ano);

        public void SetData(int dia, int mes, int ano)
        {
            if (dia < 1 || dia > 31) throw new ArgumentOutOfRangeException(nameof(dia));
            if (mes < 1 || mes > 12) throw new ArgumentOutOfRangeException(nameof(mes));
            if (ano < 1) throw new ArgumentOutOfRangeException(nameof(ano));

            Dia = dia; Mes = mes; Ano = ano;
        }

        public override string ToString() => $"{Dia:D2}/{Mes:D2}/{Ano:D4}";
    }
}

