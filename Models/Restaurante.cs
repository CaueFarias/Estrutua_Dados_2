namespace RestauranteApp.Models;

public class Restaurante
{
    private readonly List<Pedido> pedidos = new();
    private int proxPedido = 1;

    public Pedido? NovoPedido(string cliente)
    {
        if (pedidos.Count >= 50) return null;

        var pedido = new Pedido
        {
            Id = proxPedido++,
            Cliente = cliente
        };

        pedidos.Add(pedido);
        return pedido;
    }

    public Pedido? BuscarPedido(int id)
        => pedidos.FirstOrDefault(p => p.Id == id);

    public bool CancelarPedido(int id)
    {
        var pedido = BuscarPedido(id);
        if (pedido is null) return false;
        return pedidos.Remove(pedido);
    }

    public IReadOnlyList<Pedido> GetPedidos() => pedidos;
}
