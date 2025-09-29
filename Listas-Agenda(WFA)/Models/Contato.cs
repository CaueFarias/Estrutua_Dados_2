using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas_Agenda_WFA_.Models
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public Data DtNasc { get; set; }
        public List<Telefone> Telefones { get; private set; }

        public Contato(string nome, string email, Data dtNasc)
        {
            Nome = nome;
            Email = email;
            DtNasc = dtNasc;
            Telefones = new List<Telefone>();
        }

        public void AdicionarTelefone(Telefone t)
        {
            if (t.Principal)
                foreach (var tel in Telefones) tel.Principal = false;

            Telefones.Add(t);
        }

        public string GetTelefonePrincipal()
        {
            var tel = Telefones.FirstOrDefault(t => t.Principal);
            return tel != null ? tel.Numero : "Nenhum";
        }

        public int GetIdade()
        {
            var hoje = DateTime.Now;
            var nasc = new DateTime(DtNasc.Ano, DtNasc.Mes, DtNasc.Dia);
            int idade = hoje.Year - nasc.Year;
            if (hoje.Month < nasc.Month || (hoje.Month == nasc.Month && hoje.Day < nasc.Day))
                idade--;
            return idade;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Nome: {Nome}");
            sb.AppendLine($"Email: {Email}");
            sb.AppendLine($"Nascimento: {DtNasc} (Idade: {GetIdade()} anos)");
            sb.AppendLine("Telefones:");
            foreach (var t in Telefones)
                sb.AppendLine($" - {t}");
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is Contato c && c.Email.Equals(Email, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Email.GetHashCode();
    }
}

