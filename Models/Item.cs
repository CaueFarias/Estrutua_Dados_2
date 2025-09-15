namespace RestauranteApp.Models;

public class Item
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public double Preco { get; set; }

    public override string ToString()
        => $"{Id} - {Descricao} (R$ {Preco:F2})";
}

