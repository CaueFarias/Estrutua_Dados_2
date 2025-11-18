using System;

internal class Program
{
    static void Main()
    {
        Cadastro cadastro = new();
        cadastro.Download(); // carrega dados

        int op;

        do
        {
            Console.Clear();
            Console.WriteLine("---- SISTEMA DE ACESSOS ----");
            Console.WriteLine("1. Cadastrar ambiente");
            Console.WriteLine("2. Consultar ambiente");
            Console.WriteLine("3. Excluir ambiente");
            Console.WriteLine("4. Cadastrar usuario");
            Console.WriteLine("5. Consultar usuario");
            Console.WriteLine("6. Excluir usuario");
            Console.WriteLine("7. Conceder permissão");
            Console.WriteLine("8. Revogar permissão");
            Console.WriteLine("9. Registrar acesso");
            Console.WriteLine("10. Consultar logs");
            Console.WriteLine("0. Sair");
            Console.Write("Opção: ");
            op = int.Parse(Console.ReadLine() ?? "0");

            switch (op)
            {
                case 1:
                    CadastrarAmbiente(cadastro);
                    break;

                case 2:
                    ConsultarAmbiente(cadastro);
                    break;

                case 3:
                    ExcluirAmbiente(cadastro);
                    break;

                case 4:
                    CadastrarUsuario(cadastro);
                    break;

                case 5:
                    ConsultarUsuario(cadastro);
                    break;

                case 6:
                    ExcluirUsuario(cadastro);
                    break;

                case 7:
                    ConcederPermissao(cadastro);
                    break;

                case 8:
                    RevogarPermissao(cadastro);
                    break;

                case 9:
                    RegistrarAcesso(cadastro);
                    break;

                case 10:
                    ConsultarLogs(cadastro);
                    break;
            }

            if (op != 0)
            {
                Console.WriteLine("\nPressione ENTER...");
                Console.ReadLine();
            }

        } while (op != 0);

        cadastro.Upload();
    }

    // -----------------------------
    // FUNÇÕES DO MENU
    // -----------------------------

    static void CadastrarAmbiente(Cadastro c)
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        c.AdicionarAmbiente(new Ambiente(id, nome));
        Console.WriteLine("Ambiente cadastrado!");
    }

    static void ConsultarAmbiente(Cadastro c)
    {
        Console.Write("ID do ambiente: ");
        int id = int.Parse(Console.ReadLine());
        var ambiente = c.PesquisarAmbiente(new Ambiente(id, ""));

        if (ambiente == null)
        {
            Console.WriteLine("Não encontrado!");
            return;
        }

        Console.WriteLine(ambiente);
    }

    static void ExcluirAmbiente(Cadastro c)
    {
        Console.Write("ID do ambiente: ");
        int id = int.Parse(Console.ReadLine());
        bool ok = c.RemoverAmbiente(new Ambiente(id, ""));

        Console.WriteLine(ok ? "Removido!" : "Não foi possível remover.");
    }

    static void CadastrarUsuario(Cadastro c)
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        c.AdicionarUsuario(new Usuario(id, nome));
        Console.WriteLine("Usuário cadastrado!");
    }

    static void ConsultarUsuario(Cadastro c)
    {
        Console.Write("ID do usuário: ");
        int id = int.Parse(Console.ReadLine());

        var u = c.PesquisarUsuario(new Usuario(id, ""));

        if (u == null) { Console.WriteLine("Não encontrado!"); return; }

        Console.WriteLine(u);
    }

    static void ExcluirUsuario(Cadastro c)
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());

        bool ok = c.RemoverUsuario(new Usuario(id, ""));
        Console.WriteLine(ok ? "Usuário removido!" : "Usuário possui permissões ativas!");
    }

    static void ConcederPermissao(Cadastro c)
    {
        Console.Write("ID Usuário: ");
        int idU = int.Parse(Console.ReadLine());
        Console.Write("ID Ambiente: ");
        int idA = int.Parse(Console.ReadLine());

        var u = c.PesquisarUsuario(new Usuario(idU, ""));
        var a = c.PesquisarAmbiente(new Ambiente(idA, ""));

        if (u == null || a == null)
        {
            Console.WriteLine("Usuário/Ambiente não encontrado!");
            return;
        }

        bool ok = u.ConcederPermissao(a);
        Console.WriteLine(ok ? "Permissão concedida!" : "Usuário já possui essa permissão.");
    }

    static void RevogarPermissao(Cadastro c)
    {
        Console.Write("ID Usuário: ");
        int idU = int.Parse(Console.ReadLine());
        Console.Write("ID Ambiente: ");
        int idA = int.Parse(Console.ReadLine());

        var u = c.PesquisarUsuario(new Usuario(idU, ""));
        var a = c.PesquisarAmbiente(new Ambiente(idA, ""));

        if (u == null || a == null)
        {
            Console.WriteLine("Usuário/Ambiente não encontrado!");
            return;
        }

        bool ok = u.RevogarPermissao(a);
        Console.WriteLine(ok ? "Permissão revogada!" : "Usuário não tinha essa permissão.");
    }

    static void RegistrarAcesso(Cadastro c)
    {
        Console.Write("ID Usuário: ");
        int idU = int.Parse(Console.ReadLine());
        Console.Write("ID Ambiente: ");
        int idA = int.Parse(Console.ReadLine());

        var u = c.PesquisarUsuario(new Usuario(idU, ""));
        var a = c.PesquisarAmbiente(new Ambiente(idA, ""));

        if (u == null || a == null)
        {
            Console.WriteLine("Usuário/Ambiente não encontrado!");
            return;
        }

        bool autorizado = u.Ambientes.Any(x => x.Id == a.Id);

        a.RegistrarLog(new Log(u, autorizado));

        Console.WriteLine(autorizado ? "ACESSO AUTORIZADO" : "ACESSO NEGADO");
    }

    static void ConsultarLogs(Cadastro c)
    {
        Console.Write("ID Ambiente: ");
        int id = int.Parse(Console.ReadLine());

        var a = c.PesquisarAmbiente(new Ambiente(id, ""));

        if (a == null)
        {
            Console.WriteLine("Ambiente inexistente!");
            return;
        }

        Console.WriteLine("1 - Autorizados");
        Console.WriteLine("2 - Negados");
        Console.WriteLine("3 - Todos");

        int tipo = int.Parse(Console.ReadLine());

        foreach (var log in a.Logs)
        {
            if (tipo == 1 && !log.TipoAcesso) continue;
            if (tipo == 2 && log.TipoAcesso) continue;

            Console.WriteLine(log);
        }
    }
}
