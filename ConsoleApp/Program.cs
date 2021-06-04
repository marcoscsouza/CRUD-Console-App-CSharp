using Data;
using Domain;
using System;
using System.Globalization;

namespace ConsoleApp
{
    class Program
    {
        public const string continuar = "Pressione qualquer tecla para exibir o menu principal ...";

        static void Main(string[] args)
        {
            string opcaoMenu;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            do
            {
                ExibirMenuPrincipal();
                opcaoMenu = Console.ReadLine();

                switch (opcaoMenu)
                {
                    case "1": //colocar a função consultar, deletar e atualizar tudo nesse menu
                        ConsultarBanda();
                        break;
                    case "2":   
                        InserirBanda();
                        break;
                    case "3":   // função deletar completa
                        ListarBanda();  //falta função atualizar...
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Voce escolheu sair do programa.");
                        break;
                    default:
                        Console.WriteLine($"Opção inválida! escolha outra. {continuar}");
                        Console.ReadKey();
                        break;
                }

            } while (opcaoMenu != "4");
        }

        static void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("**********Gerenciador de Bandas**********");
            Console.WriteLine("Selecione uma das opções abaixo: ");
            Console.WriteLine("1 - Pesquisar por bandas");
            Console.WriteLine("2 - Adicionar uma nova banda");
            Console.WriteLine("3 - Lista completa de bandas");
            Console.WriteLine("4 - Sair");
        }

        static void ConsultarBanda()
        {
            var repoBanda = new BandaRepository();

            Console.Clear();
            Console.WriteLine("Qual banda deseja pesquisar?");
            var pesquisarBanda = Console.ReadLine();
            var bandasEncontradas = repoBanda.Pesquisar(pesquisarBanda);

            if (bandasEncontradas.Count > 0)
            {
                Console.WriteLine("Selecione abaixo uma das opções para visualizar os dados das bandas encontradas: ");
                for (var i = 0; i < bandasEncontradas.Count; i++)
                    Console.WriteLine($"{i} - {bandasEncontradas[i].NomeBanda}");

                if (!ushort.TryParse(Console.ReadLine(), out var EscolherI) || EscolherI >= bandasEncontradas.Count)
                {
                    Console.WriteLine($"Opção inválida! {continuar}");
                    Console.ReadKey();
                    return;
                }

                if (EscolherI < bandasEncontradas.Count)
                {
                    var banda = bandasEncontradas[EscolherI];

                    Console.WriteLine("Dados da banda:");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine($"Identificador único: {banda.IdBanda}");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine($"Nome da banda: {banda.NomeBanda}");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine($"Data da criação: {banda.InicioBanda:d}");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine($"Integrantes da banda: {banda.ParticipantesBanda}");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine($"Banda continua na ativa: {banda.FazendoShow}");
                    Console.WriteLine("-----------------------------------------------------------");

                    var diasDoShowDeAniversario = BandaModel.ShowDeAniversario(banda.InicioBanda);
                    var anosNaAtiva = BandaModel.AnosDeCarreira(banda.InicioBanda);
                    Console.WriteLine(BandaModel.MensagemShowDeAniversario(diasDoShowDeAniversario, anosNaAtiva));

                    Console.Write(continuar);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine($"Não foi encontrada nenhuma banda! {continuar}");
                Console.ReadKey();
            }
        }

        public static void InserirBanda()
        {
            var repoBanda = new BandaRepository();

            Console.Clear();
            Console.WriteLine("Informe o nome da banda que deseja adicionar: ");
            var nomeBanda = Console.ReadLine();
            if (nomeBanda.Length < 1)
            {
                Console.WriteLine($"Nome inválido! Dados descartados! {continuar}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Informe o dia de inicio da banda no formato (dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out var inicioBanda))
            {
                Console.WriteLine($"Data inválida! Dados descartados! {continuar}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Informe quantos integrantes a banda possui (Números): ");
            if (!Int32.TryParse(Console.ReadLine(), out var participantesBanda))
            {
                Console.WriteLine($"Numero inválido! Dados descartados! {continuar}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Informe se a banda continua fazendo show (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out var fazendoShow))
            {
                Console.WriteLine($"Valor inválido! Dados descartados!{continuar}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Os dados estão corretos?");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"Nome da banda: {nomeBanda}");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"Inicio da banda: {inicioBanda:d}");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"Integrantes da banda: {participantesBanda}");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"Banda continua fazendo show: {fazendoShow}");
            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("1 - Sim \n2 - Não");
            var opcaoselecionada = Console.ReadLine();

            if (opcaoselecionada == "1")
            {
                var identificador = Guid.NewGuid();
                repoBanda.Adicionar(new BandaModel(identificador, nomeBanda, inicioBanda, participantesBanda, fazendoShow));

                Console.WriteLine($"Dados adicionados com sucesso! {continuar}");
                Console.ReadKey();
            }
            else if (opcaoselecionada == "2")
            {
                Console.WriteLine($"Todos os dados foram descartados!{continuar}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Opção inválida! {continuar}");
                Console.ReadKey();
            }
        }

        public static void ListarBanda()
        {
            var repoBanda = new BandaRepository();

            Console.Clear();
            Console.WriteLine("Lista de bandas");
            int x = 0;
            foreach (BandaModel bandaModel in repoBanda.ListarTodos())  // listar todos os itens da lista
            {
                Console.WriteLine($"{x} - nome: {bandaModel.NomeBanda}");
                x++;
            }

            Console.WriteLine("Digite o nome de qual banda deseja deletar?");
            var deletarBanda = Console.ReadLine();
            var escolhaBanda = repoBanda.EscolherNome(deletarBanda);

            if (escolhaBanda == null)   // se escolha der nulo
            {
                Console.WriteLine($"Opção inválida! {continuar}");
                Console.ReadKey();
            }
            else    // achando o resultado
            { 
                Console.WriteLine("Dados da banda:");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Identificador único: {escolhaBanda.IdBanda}");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Nome da banda: {escolhaBanda.NomeBanda}");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Data da criação: {escolhaBanda.InicioBanda:d}");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Integrantes da banda: {escolhaBanda.ParticipantesBanda}");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Banda continua na ativa: {escolhaBanda.FazendoShow}");
                Console.WriteLine("-----------------------------------------------------------");

                Console.WriteLine("quer mesmo deletar esta banda?");
                Console.WriteLine("1 - Sim \n2 - Não");
                var opcaoDeletar = Console.ReadLine();
                if (opcaoDeletar == "1")
                {
                    repoBanda.Remover(deletarBanda);   
                    Console.WriteLine($"Dados adicionados com sucesso! {continuar}");
                    Console.ReadKey();
                    return;
                }
                else if (opcaoDeletar == "2")
                {
                    Console.WriteLine($"Todos os dados foram descartados!{continuar}");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Opção inválida! {continuar}");
                    Console.ReadKey();
                }
            }
        }
    }
}
