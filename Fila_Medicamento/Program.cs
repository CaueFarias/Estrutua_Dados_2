using Fila_Medicamento;
using System;
using System.Globalization;

namespace Fila_Medicamento
{
    internal class Program
    {
        static Medicamentos medicamentos = new Medicamentos();

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== MENU DE OPÇÕES ===");
                Console.WriteLine("0. Finalizar processo");
                Console.WriteLine("1. Cadastrar medicamento");
                Console.WriteLine("2. Consultar medicamento (sintético)");
                Console.WriteLine("3. Consultar medicamento (analítico)");
                Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
                Console.WriteLine("5. Vender medicamento (abater do lote mais antigo)");
                Console.WriteLine("6. Listar medicamentos");
                Console.Write("Opção: ");
                opcao = int.Parse(Console.ReadLine() ?? "0");

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando...");
                        break;
                    case 1:
                        CadastrarMedicamento();
                        break;
                    case 2:
                        ConsultarMedicamentoSintetico();
                        break;
                    case 3:
                        ConsultarMedicamentoAnalitico();
                        break;
                    case 4:
                        ComprarMedicamento();
                        break;
                    case 5:
                        VenderMedicamento();
                        break;
                    case 6:
                        ListarMedicamentos();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

            } while (opcao != 0);
        }

        static void CadastrarMedicamento()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Laboratório: ");
            string lab = Console.ReadLine() ?? "";

            Medicamento med = new Medicamento(id, nome, lab);
            medicamentos.Adicionar(med);
            Console.WriteLine("Medicamento cadastrado!");
        }

        static void ConsultarMedicamentoSintetico()
        {
            Medicamento med = BuscarMedicamento();
            if (med != null)
                Console.WriteLine(med);
            else
                Console.WriteLine("Medicamento não encontrado.");
        }

        static void ConsultarMedicamentoAnalitico()
        {
            Medicamento med = BuscarMedicamento();
            if (med != null)
            {
                Console.WriteLine(med);
                foreach (var lote in med.Lotes)
                    Console.WriteLine("  " + lote);
            }
            else
                Console.WriteLine("Medicamento não encontrado.");
        }

        static void ComprarMedicamento()
        {
            Medicamento med = BuscarMedicamento();
            if (med == null)
            {
                Console.WriteLine("Medicamento não encontrado!");
                return;
            }

            Console.Write("ID do Lote: ");
            int idLote = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Quantidade: ");
            int qtde = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Data de Vencimento (dd/mm/aaaa): ");
            DateTime venc = DateTime.ParseExact(Console.ReadLine() ?? "", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            med.Comprar(new Lote(idLote, qtde, venc));
            Console.WriteLine("Lote registrado!");
        }

        static void VenderMedicamento()
        {
            Medicamento med = BuscarMedicamento();
            if (med == null)
            {
                Console.WriteLine("Medicamento não encontrado!");
                return;
            }

            Console.Write("Quantidade a vender: ");
            int qtde = int.Parse(Console.ReadLine() ?? "0");

            if (med.Vender(qtde))
                Console.WriteLine("Venda realizada!");
            else
                Console.WriteLine("Estoque insuficiente.");
        }

        static void ListarMedicamentos()
        {
            Console.WriteLine("\n=== LISTA DE MEDICAMENTOS ===");
            foreach (var m in medicamentos.Listar())
                Console.WriteLine(m);
        }

        static Medicamento? BuscarMedicamento()
        {
            Console.Write("Informe o ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            return medicamentos.Pesquisar(new Medicamento(id, "", ""));
        }
    }
}
