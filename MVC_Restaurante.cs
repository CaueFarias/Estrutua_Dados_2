using RestauranteApp.Models;

var restaurante = new Restaurante();
int opcao;

do
{
    Console.WriteLine("\n--- MENU ---");
    Console.WriteLine("0. Sair");
    Console.WriteLine("1. Criar novo pedido");
    Console.WriteLine("2. Adicionar item ao pedido");
    Console.WriteLine("3. Remover item do pedido");
    Console.WriteLine("4. Consultar pedido");
    Console.WriteLine("5. Cancelar pedido");
    Console.WriteLine("6. Listar todos os pedidos");
    Console.Write("Escolha: ");
    _ = int.TryParse(Console.ReadLine(), out opcao);

    switch (opcao)
    {
        case 1: CriarPedido(); break;
        case 2: AdicionarItem(); break;
        case 3: RemoverItem(); break;
        case 4: ConsultarPedido(); break;
        case 5: CancelarPedido(); break;
        case 6: ListarPedidos(); break;
    }
} while (opcao != 0);

void CriarPedido()
{
    Console.Write("Nome do cliente: ");
    string cliente = Console.ReadLine()!;

    var pedido = restaurante.NovoPedido(cliente);
    Console.WriteLine(pedido is not null
        ? $"Pedido {pedido.Id} criado com sucesso."
        : "Não foi possível criar pedido (limite 50).");
}

void AdicionarItem()
{
    Console.Write("Id do pedido: ");
    int id = int.Parse(Console.ReadLine()!);
    var pedido = restaurante.BuscarPedido(id);
    if (pedido is null) { Console.WriteLine("Pedido não encontrado."); return; }

    Console.Write("Id do item: ");
    int idItem = int.Parse(Console.ReadLine()!);
    Console.Write("Descrição: ");
    string desc = Console.ReadLine()!;
    Console.Write("Preço: ");
    double preco = double.Parse(Console.ReadLine()!);

    var item = new Item { Id = idItem, Descricao = desc, Preco = preco };
    Console.WriteLine(pedido.AdicionarItem(item)
        ? "Item adicionado."
        : "Limite de 10 itens atingido.");
}

void RemoverItem()
{
    Console.Write("Id do pedido: ");
    int id = int.Parse(Console.ReadLine()!);
    var pedido = restaurante.BuscarPedido(id);
    if (pedido is null) { Console.WriteLine("Pedido não encontrado."); return; }

    Console.Write("Id do item a remover: ");
    int idItem = int.Parse(Console.ReadLine()!);

    Console.WriteLine(pedido.RemoverItem(idItem)
        ? "Item removido."
        : "Item não encontrado.");
}

void ConsultarPedido()
{
    Console.Write("Id do pedido: ");
    int id = int.Parse(Console.ReadLine()!);
    var pedido = restaurante.BuscarPedido(id);

    Console.WriteLine(pedido is not null
        ? pedido.DadosDoPedido()
        : "Pedido não encontrado.");
}

void CancelarPedido()
{
    Console.Write("Id do pedido: ");
    int id = int.Parse(Console.ReadLine()!);

    Console.WriteLine(restaurante.CancelarPedido(id)
        ? "Pedido cancelado."
        : "Pedido não encontrado.");
}

void ListarPedidos()
{
    var pedidos = restaurante.GetPedidos();
    double soma = 0;

    foreach (var p in pedidos)
    {
        double total = p.CalcularTotal();
        soma += total;
        Console.WriteLine($"Pedido {p.Id} - Cliente: {p.Cliente} - Total: R$ {total:F2}");
    }

    Console.WriteLine($"Soma geral do dia: R$ {soma:F2}");
}
