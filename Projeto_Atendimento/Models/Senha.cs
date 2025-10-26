using System;

namespace Projeto_Atendimento.Models
{
    public class Senha
    {
        public int Id { get; set; }
        public DateTime DataGerac { get; set; }
        public DateTime HoraGerac { get; set; }
        public DateTime DataAtend { get; set; }
        public DateTime HoraAtend { get; set; }

        public Senha(int id)
        {
            Id = id;
            DataGerac = DateTime.Now;
            HoraGerac = DateTime.Now;
        }

        public string dadosParciais()
        {
            return $"{Id} - {DataGerac:dd/MM/yyyy} - {HoraGerac:HH:mm:ss}";
        }

        public string dadosCompletos()
        {
            return $"{Id} - {DataGerac:dd/MM/yyyy} - {HoraGerac:HH:mm:ss} - {DataAtend:dd/MM/yyyy} - {HoraAtend:HH:mm:ss}";
        }
    }
}
