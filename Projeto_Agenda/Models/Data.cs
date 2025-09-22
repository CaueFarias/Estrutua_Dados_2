using System;

namespace Projeto_Agenda.Models
{
    public class Data
    {
        public int Dia { get; private set; } = 1;
        public int Mes { get; private set; } = 1;
        public int Ano { get; private set; } = 2000;

        public Data() { }

        public Data(int dia, int mes, int ano)
        {
            SetData(dia, mes, ano);
        }

        public void SetData(int dia, int mes, int ano)
        {
            if (dia < 1 || dia > 31)
                throw new ArgumentOutOfRangeException(nameof(dia), "Dia deve estar entre 1 e 31.");
            if (mes < 1 || mes > 12)
                throw new ArgumentOutOfRangeException(nameof(mes), "Mês deve estar entre 1 e 12.");
            if (ano < 1)
                throw new ArgumentOutOfRangeException(nameof(ano), "Ano deve ser positivo.");

            Dia = dia;
            Mes = mes;
            Ano = ano;
        }

        public override string ToString()
        {
            return $"{Dia:D2}/{Mes:D2}/{Ano:D4}";
        }
    }
}
