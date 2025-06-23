using Biblioteca.ConsoleUI;
using Biblioteca.Domain;


public class Emprestimo
{
    public Leitor Leitor { get; set; }
    public Livros Livro { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }

    public bool Ativo => DataDevolucao == null;

    public static List<Emprestimo> ListaEmprestimos { get; set; } = new List<Emprestimo>();

    private static string CaminhoArquivo = "emprestimos.json";

    public static void SalvarEmArquivo()
    {
        var json = JsonSerializer.Serialize(ListaEmprestimos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, json);
    }

    public static void CarregarDeArquivo()
    {
        if (File.Exists(CaminhoArquivo))
        {
            string json = File.ReadAllText(CaminhoArquivo);
            ListaEmprestimos = JsonSerializer.Deserialize<List<Emprestimo>>(json) ?? new List<Emprestimo>();
        }
    }

    public static void RealizarEmprestimo()
    {
        int matricula = Program.LerInteiro("Matr�cula do leitor: ");

        var leitor = Leitor.BuscarLeitorPorMatricula(matricula);
        if (leitor == null)
        {
            Console.WriteLine("Leitor n�o encontrado.");
            return;
        }

        Console.Write("T�tulo do livro: ");
        string titulo = Console.ReadLine();

        var livro = Livros.BuscarLivroPorNome(titulo);
        if (livro == null)
        {
            Console.WriteLine("Livro n�o encontrado.");
            return;
        }

        bool estaEmprestado = Emprestimo.ListaEmprestimos.Any(e => e.Livro.Titulo == titulo && e.Ativo);
        if (estaEmprestado)
        {
            Console.WriteLine("Este livro j� est� emprestado.");
            return;
        }

        var emprestimo = new Emprestimo
        {
            Leitor = leitor,
            Livro = livro,
            DataEmprestimo = DateTime.Now
        };

        Emprestimo.ListaEmprestimos.Add(emprestimo);
        Emprestimo.SalvarEmArquivo();
        Console.WriteLine("Empr�stimo realizado com sucesso!");
    }

    public static void RegistrarDevolucao()
    {
        Console.Write("T�tulo do livro a devolver: ");
        string titulo = Console.ReadLine();

        var emprestimo = Emprestimo.ListaEmprestimos
            .FirstOrDefault(e => e.Livro.Titulo == titulo && e.Ativo);

        if (emprestimo == null)
        {
            Console.WriteLine("Empr�stimo ativo n�o encontrado para este livro.");
            return;
        }

        emprestimo.DataDevolucao = DateTime.Now;
        Emprestimo.SalvarEmArquivo();
        Console.WriteLine("Livro devolvido com sucesso!");
    }

    public static void ListarEmprestimosAtivos()
    {
        var ativos = Emprestimo.ListaEmprestimos.Where(e => e.Ativo).ToList();

        if (ativos.Count == 0)
        {
            Console.WriteLine("Nenhum empr�stimo ativo.");
            return;
        }

        foreach (var e in ativos)
        {
            Console.WriteLine($"Livro: {e.Livro.Titulo}, Leitor: {e.Leitor.Nome}, Empr�stimo em: {e.DataEmprestimo:d}");
        }
    }

    public static void ListarEmprestimosConcluidos()
    {
        var devolvidos = ListaEmprestimos.Where(e => !e.Ativo).ToList();

        if (devolvidos.Count == 0)
        {
            Console.WriteLine("Nenhum empr�stimo conclu�do.");
            return;
        }

        foreach (var e in devolvidos)
        {
            Console.WriteLine($"Livro: {e.Livro.Titulo}, Leitor: {e.Leitor.Nome}, Devolvido em: {e.DataDevolucao:d}");
        }
    }

    public static void QuantidadeEmprestimosAtivos()
    {
        int total = ListaEmprestimos.Count(e => e.Ativo);
        Console.WriteLine($"Total de livros emprestados no momento: {total}");
    }

    public static void LeitorMaisAtivo()
    {
        var agrupado = ListaEmprestimos
            .GroupBy(e => e.Leitor.Nome)
            .Select(g => new { Nome = g.Key, Quantidade = g.Count() })
            .OrderByDescending(g => g.Quantidade)
            .FirstOrDefault();

        if (agrupado != null)
        {
            Console.WriteLine($"Leitor com mais empr�stimos: {agrupado.Nome} ({agrupado.Quantidade} empr�stimos)");
        }
        else
        {
            Console.WriteLine("Nenhum empr�stimo registrado ainda.");
        }
    }

    public static void LivroMaisEmprestado()
    {
        var agrupado = ListaEmprestimos
            .GroupBy(e => e.Livro.Titulo)
            .Select(g => new { Titulo = g.Key, Quantidade = g.Count() })
            .OrderByDescending(g => g.Quantidade)
            .FirstOrDefault();

        if (agrupado != null)
        {
            Console.WriteLine($"Livro mais emprestado: {agrupado.Titulo} ({agrupado.Quantidade} vezes)");
        }
        else
        {
            Console.WriteLine("Nenhum empr�stimo registrado ainda.");
        }
    }
}