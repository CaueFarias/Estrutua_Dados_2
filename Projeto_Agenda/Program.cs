using Projeto_Agenda.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Contatos agenda = new();

        int opcao;
        do
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar contato");
            Console.WriteLine("2. Pesquisar contato");
            Console.WriteLine("3. Alterar contato");
            Console.WriteLine("4. Remover contato");
            Console.WriteLine("5. Listar contatos");
            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine() ?? "0");

            switch (opcao)
            {
                case 1:
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    string email = Console.ReadLine() ?? "";
                    Console.Write("Data de nascimento (dd/mm/aaaa): ");
                    var partes = Console.ReadLine()?.Split('/') ?? new string[] { "1", "1", "2000" };
                    Data dt = new(int.Parse(partes[0]), int.Parse(partes[1]), int.Parse(partes[2]));

                    Contato c = new(nome, email, dt);

                    Console.Write("Telefone principal (s/n)? ");
                    bool principal = Console.ReadLine()?.ToLower() == "s";
                    Console.Write("Tipo: ");
                    string tipo = Console.ReadLine() ?? "Celular";
                    Console.Write("Número: ");
                    string numero = Console.ReadLine() ?? "";
                    c.AdicionarTelefone(new Telefone(tipo, numero, principal));

                    if (agenda.Adicionar(c))
                        Console.WriteLine("Contato adicionado!");
                    else
                        Console.WriteLine("Já existe contato com esse email.");
                    break;

                case 2:
                    Console.Write("Digite o email do contato: ");
                    email = Console.ReadLine() ?? "";
                    c = agenda.Pesquisar(new Contato("", email, new Data(1, 1, 2000)));
                    Console.WriteLine(c != null ? c : "Contato não encontrado.");
                    break;

                case 3:
                    Console.Write("Digite o email do contato para alterar: ");
                    email = Console.ReadLine() ?? "";
                    var existente = agenda.Pesquisar(new Contato("", email, new Data(1, 1, 2000)));
                    if (existente == null)
                    {
                        Console.WriteLine("Contato não encontrado.");
                        break;
                    }

                    Console.Write("Novo nome: ");
                    nome = Console.ReadLine() ?? existente.Nome;
                    Console.Write("Nova data de nascimento (dd/mm/aaaa): ");
                    partes = Console.ReadLine()?.Split('/') ?? new string[] { existente.DtNasc.Dia.ToString(), existente.DtNasc.Mes.ToString(), existente.DtNasc.Ano.ToString() };
                    dt = new(int.Parse(partes[0]), int.Parse(partes[1]), int.Parse(partes[2]));
                    existente.Nome = nome;
                    existente.DtNasc = dt;

                    Console.WriteLine("Contato alterado.");
                    break;

                case 4:
                    Console.Write("Digite o email do contato para remover: ");
                    email = Console.ReadLine() ?? "";
                    if (agenda.Remover(new Contato("", email, new Data(1, 1, 2000))))
                        Console.WriteLine("Contato removido.");
                    else
                        Console.WriteLine("Contato não encontrado.");
                    break;

                case 5:
                    foreach (var contato in agenda.Agenda)
                        Console.WriteLine("\n" + contato);
                    break;
            }

        } while (opcao != 0);
    }
}