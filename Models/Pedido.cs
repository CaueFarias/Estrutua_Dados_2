namespace RestauranteApp.Models;

public class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    private readonly List<Item> itens = new();

    public bool AdicionarItem(Item item)
    {
        if (itens.Count >= 10) return false;
        itens.Add(item);
        return true;
    }

    public bool RemoverItem(int itemId)
    {
        var item = itens.FirstOrDefault(i => i.Id == itemId);
        if (item is null) return false;
        itens.Remove(item);
        return true;
    }

    public double CalcularTotal()
        => itens.Sum(i => i.Preco);

    public string DadosDoPedido()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Pedido {Id} - Cliente: {Cliente}");
        foreach (var item in itens)
            sb.AppendLine($"  {item}");
        sb.AppendLine($"Total: R$ {CalcularTotal():F2}");
        return sb.ToString();
    }

    public IReadOnlyList<Item> GetItens() => itens;
}
