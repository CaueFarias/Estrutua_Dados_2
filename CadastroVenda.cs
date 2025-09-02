namespace ProjetoVendas.Models
{
    public class Venda
    {
        public int Qtde { get; set; }
        public double Valor { get; set; }

        public Venda(int qtde, double valor)
        {
            Qtde = qtde;
            Valor = valor;
        }

        public double ValorMedio()
        {
            return Qtde > 0 ? Valor / Qtde : 0;
        }
    }
}
