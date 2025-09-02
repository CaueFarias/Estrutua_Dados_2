using System.Collections.Generic;
using System.Linq;

namespace ProjetoVendas.Models
{
    public class Vendedores
    {
        private List<Vendedor> osVendedores = new List<Vendedor>();
        private int max = 10;

        public bool AddVendedor(Vendedor v)
        {
            if (osVendedores.Count >= max) return false;
            osVendedores.Add(v);
            return true;
        }

        public bool DelVendedor(Vendedor v)
        {
            if (v.TemVendas()) return false;
            return osVendedores.Remove(v);
        }

        public Vendedor SearchVendedor(int id)
        {
            return osVendedores.FirstOrDefault(x => x.Id == id);
        }

        public double ValorVendas()
        {
            return osVendedores.Sum(v => v.ValorVendas());
        }

        public double ValorComissao()
        {
            return osVendedores.Sum(v => v.ValorComissao());
        }

        public List<Vendedor> Listar()
        {
            return osVendedores;
        }
    }
}
