using System.Numerics;
using System.Text.Json;
using Biblioteca.Domain;
using Biblioteca.Services;

namespace Biblioteca_Console
{
    internal class Program
    {
        static void Main()
        {
            Leitor.CarregarDeArquivo();
            Livros.CarregarDeArquivo();
            Emprestimo.CarregarDeArquivo();

            bool executando = true;

            while (executando)
            {
                Console.WriteLine("====== Biblioteca Console =======");
                Console.WriteLine("1. Livros");
                Console.WriteLine("2. Leitores");
                Console.WriteLine("3. Empréstimos");
                Console.WriteLine("4. Sair");
                int opcao = Program.LerInteiro("Digite a opção: ");

                switch (opcao)
                {
                    case 1:
                        MenuDeLivros();
                        break;

                    case 2:
                        MenuDeLeitores();
                        break;
                    case 3:
                        MenuDeEmprestimos();
                        break;
                    case 4:
                        executando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }


            }

        }

        public static int LerInteiro(string mensagem)
        {
            int numero;
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out numero))
                    return numero;
                else
                    Console.WriteLine("Valor inválido. Digite um número inteiro.");
            }
        }



        public static void MenuDeLivros()
        {
            CadastroLivro cadastroLivro = new CadastroLivro();

            bool emMenu = true;

            while (emMenu)
            {

                Console.WriteLine("=== Livros ===");
                Console.WriteLine("1. Cadastrar");
                Console.WriteLine("2. Editar");
                Console.WriteLine("3. Remover");
                Console.WriteLine("4. Buscar por Nome");
                Console.WriteLine("5. Listar Livros");
                Console.WriteLine("6. Voltar");
                int opcao = Program.LerInteiro("Digite a opção: ");

                switch (opcao)
                {
                    case 1:
                        cadastroLivro.AddLivro();
                        break;

                    case 2:
                        Console.Write("Título atual: ");
                        string titulo = Console.ReadLine();

                        Console.Write("Novo título: ");
                        string novoTitulo = Console.ReadLine();

                        Console.Write("Novo autor: ");
                        string novoAutor = Console.ReadLine();

                        int novoAno = Program.LerInteiro("Novo ano: ");

                        bool sucesso = Livros.EditarLivro(titulo, novoTitulo, novoAutor, novoAno);
                        if (sucesso)
                        {
                            Livros.SalvarEmArquivo();
                            Console.WriteLine("Livro editado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Livro não encontrado.");
                        }

                        break;

                    case 3:
                        Console.Write("Digite o título do livro que deseja excluir: ");
                        string tituloExcluir = Console.ReadLine();

                        Console.Write("Tem certeza que deseja excluir? (s/n): ");
                        string confirmacao = Console.ReadLine();
                        if (confirmacao.ToLower() != "s")
                        {
                            bool excluido = Livros.ExcluirLivro(tituloExcluir);
                            if (excluido)
                            {
                                Livros.SalvarEmArquivo();
                                Console.WriteLine("Livro excluído com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Livro não encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Exclusão cancelada.");
                        }

                         break;

                    case 4:
                        Console.WriteLine("Digite o nome do livro: ");
                        string buscarLivro = Console.ReadLine();

                        var livro = Livros.BuscarLivroPorNome(buscarLivro);
                        if (livro != null)
                        {
                            Console.WriteLine($"Livro encontrado:");
                            Console.WriteLine($"Título: {livro.Titulo}");
                            Console.WriteLine($"Autor: {livro.Autor}");
                            Console.WriteLine($"Ano: {livro.Ano}");
                        }
                        else
                        {
                            Console.WriteLine("Livro não encontrado.");
                        }
                        break;

                    case 5:
                        Livros.ListarLivros();
                        break;

                    case 6:
                        emMenu = false;
                        break;

                }
            }
        }

        public static void MenuDeLeitores()
        {
            CadastroLeitor cadastroLeitor = new CadastroLeitor();

            bool emMenu = true;

            while (emMenu)
            {
                Console.WriteLine("=== Leitor ===");
                Console.WriteLine("1. Cadastrar");
                Console.WriteLine("2. Editar");
                Console.WriteLine("3. Remover");
                Console.WriteLine("4. Buscar por Nome");
                Console.WriteLine("5. Buscar por Matrícula");
                Console.WriteLine("6. Listar Leitores");
                Console.WriteLine("7 Voltar");
                int opcao = Program.LerInteiro("Digite a opção: ");

                switch (opcao)
                {
                    case 1:
                        cadastroLeitor.AddLeitor();
                        break;

                    case 2:
                        int matricula = Program.LerInteiro("Digite a matrícula do leitor: ");

                        Console.Write("Digite o novo nome do leitor: ");
                        string novoNome = Console.ReadLine();

                        bool editado = Leitor.EditarLeitor(matricula, novoNome);

                        if (editado)
                        {
                            Leitor.SalvarEmArquivo();
                            Console.WriteLine("Leitor editado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Leitor não encontrado.");
                        }

                        break;

                    case 3:
                        int matriculaExcluir = Program.LerInteiro("Digite a matricula do leitor que deseja excluir: ");

                        Console.Write("Tem certeza que deseja excluir? (s/n): ");
                        string confirmacao = Console.ReadLine();
                        if (confirmacao.ToLower() == "s")
                        {
                            bool removido = Leitor.ExcluirLeitor(matriculaExcluir);
                            if (removido)
                            {
                                Leitor.SalvarEmArquivo();
                                Console.WriteLine("Leitor removido com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Leitor não encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Exclusão cancelada.");
                        }
                        break;


                    case 4:
                        Console.WriteLine("Digite o nome do leitor: ");
                        string buscarLeitorNome = Console.ReadLine();

                        var leitorNome = Leitor.BuscarLeitorPorNome(buscarLeitorNome);
                        if (leitorNome != null)
                        {
                            Console.WriteLine($"Leitor encontrado:");
                            Console.WriteLine($"Nome: {leitorNome.Nome}");
                            Console.WriteLine($"Matrícula: {leitorNome.Matricula}");
                        }
                        else
                        {
                            Console.WriteLine("Leitor não encontrado.");
                        }
                        break;

                    case 5:
                        int buscarLeitorMatricula = Program.LerInteiro("Digite a matrícula do leitor: ");

                        var leitorMatricula = Leitor.BuscarLeitorPorMatricula(buscarLeitorMatricula);
                        if (leitorMatricula != null)
                        {
                            Console.WriteLine($"Leitor encontrado:");
                            Console.WriteLine($"Nome: {leitorMatricula.Nome}");
                            Console.WriteLine($"Matrícula: {leitorMatricula.Matricula}");
                        }
                        else
                        {
                            Console.WriteLine("Leitor não encontrado.");
                        }
                        break;

                    case 6:
                        Leitor.ListarLeitores();
                        break;

                    case 7:
                        emMenu = false;
                        break;
                }
            }
        }

        public static void MenuDeEmprestimos()
        {
            Console.WriteLine("=== Empréstimos ===");
            Console.WriteLine("1. Realizar Empréstimo");
            Console.WriteLine("2. Registrar Devolução");
            Console.WriteLine("3. Listar Empréstimos Ativos");
            Console.WriteLine("4. Listar Empréstimos Concluídos");
            Console.WriteLine("5. Livro mais emprestado");
            Console.WriteLine("6. Leitor com mais empréstimos");
            Console.WriteLine("7. Total de empréstimos ativos");
            Console.WriteLine("8. Voltar");

            int opcao = Program.LerInteiro("Digite a opção: ");

            switch (opcao)
            {
                case 1:
                    Emprestimo.RealizarEmprestimo();
                    break;
                case 2:
                    Emprestimo.RegistrarDevolucao();
                    break;
                case 3:
                    Emprestimo.ListarEmprestimosAtivos();
                    break;
                case 4:
                    Emprestimo.ListarEmprestimosConcluidos();
                    break;
                case 5:
                    Emprestimo.LivroMaisEmprestado();
                    break;
                case 6:
                    Emprestimo.LeitorMaisAtivo();
                    break;
                case 7:
                    Emprestimo.QuantidadeEmprestimosAtivos();
                    break;
                case 8:
                    return;
            }

        }

    }
}
