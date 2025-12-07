using System;

class Program
{
    static Frota frota = new();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== PROJETO TRANSPORTE ===");
            Console.WriteLine("0. Finalizar");
            Console.WriteLine("1. Cadastrar veículo");
            Console.WriteLine("2. Cadastrar garagem");
            Console.WriteLine("3. Iniciar jornada");
            Console.WriteLine("4. Encerrar jornada");
            Console.WriteLine("5. Liberar viagem");
            Console.WriteLine("6. Listar veículos em garagem");
            Console.WriteLine("7. Quantidade de viagens");
            Console.WriteLine("8. Listar viagens");
            Console.WriteLine("9. Passageiros transportados");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "0": return;
                case "1": CadastrarVeiculo(); break;
                case "2": CadastrarGaragem(); break;
                case "3": IniciarJornada(); break;
                case "4": frota.EncerrarJornada(); break;
                case "5": LiberarViagem(); break;
                case "6": ListarGaragem(); break;
                case "7": QuantidadeViagens(); break;
                case "8": ListarViagens(); break;
                case "9": PassageirosTransportados(); break;
            }
        }
    }

    static void CadastrarVeiculo()
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Capacidade: ");
        int cap = int.Parse(Console.ReadLine());
        frota.CadastrarVeiculo(new Veiculo(id, cap));
    }

    static void CadastrarGaragem()
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        frota.CadastrarGaragem(new Garagem(id, nome));
    }

    static void IniciarJornada()
    {
        if (frota.IniciarJornada())
            Console.WriteLine("Jornada iniciada!");
        else
            Console.WriteLine("Não foi possível iniciar.");
        Console.ReadKey();
    }

    static void LiberarViagem()
    {
        Console.Write("Origem: ");
        int o = int.Parse(Console.ReadLine());
        Console.Write("Destino: ");
        int d = int.Parse(Console.ReadLine());
        Console.Write("Passageiros: ");
        int p = int.Parse(Console.ReadLine());

        if (frota.LiberarViagem(o, d, p))
            Console.WriteLine("Viagem liberada!");
        else
            Console.WriteLine("Erro ao liberar viagem.");

        Console.ReadKey();
    }

    static void ListarGaragem()
    {
        Console.Write("Garagem ID: ");
        int id = int.Parse(Console.ReadLine());
        var g = frota.Garagens.FirstOrDefault(x => x.Id == id);

        if (g != null)
        {
            Console.WriteLine($"\n{g}\nPotencial: {g.PotencialTransporte()}");

            foreach (var v in g.PilhaVeiculos)
                Console.WriteLine(v);
        }
        Console.ReadKey();
    }

    static void QuantidadeViagens()
    {
        Console.Write("Origem: ");
        int o = int.Parse(Console.ReadLine());
        Console.Write("Destino: ");
        int d = int.Parse(Console.ReadLine());

        var origem = frota.Garagens.First(g => g.Id == o);
        var destino = frota.Garagens.First(g => g.Id == d);

        Console.WriteLine("Total: " + frota.Controle.Quantidade(origem, destino));
        Console.ReadKey();
    }

    static void ListarViagens()
    {
        Console.Write("Origem: ");
        int o = int.Parse(Console.ReadLine());
        Console.Write("Destino: ");
        int d = int.Parse(Console.ReadLine());

        var origem = frota.Garagens.First(g => g.Id == o);
        var destino = frota.Garagens.First(g => g.Id == d);

        foreach (var v in frota.Controle.Listar(origem, destino))
            Console.WriteLine(v);

        Console.ReadKey();
    }

    static void PassageirosTransportados()
    {
        Console.Write("Origem: ");
        int o = int.Parse(Console.ReadLine());
        Console.Write("Destino: ");
        int d = int.Parse(Console.ReadLine());

        var origem = frota.Garagens.First(g => g.Id == o);
        var destino = frota.Garagens.First(g => g.Id == d);

        Console.WriteLine("Passageiros: " +
            frota.Controle.PassageirosTransportados(origem, destino));

        Console.ReadKey();
    }
}
