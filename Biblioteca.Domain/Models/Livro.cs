using Biblioteca.ConsoleUI;
using Biblioteca.Services;


public class Livros
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Ano { get; set; }
    public static List<Livros> ListaDeLivros { get; private set; } = new List<Livros>();

    private static string CaminhoArquivo = "livros.json";
    public Livros() { }

    public Livros(string titulo, string autor, int ano)
    {
        Titulo = titulo;
        Autor = autor;
        Ano = ano;
    }

    public static void ListarLivros()
    {
        if (ListaDeLivros.Count == 0)
        {
            Console.WriteLine("Livro não encontrado");
            return;
        }

        foreach (var livro in ListaDeLivros)
        {
            Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Ano: {livro.Ano}");
        }
    }

    public static Livros BuscarLivroPorNome(string titulo)
    {
        return ListaDeLivros.FirstOrDefault(l => l.Titulo.IndexOf(titulo, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public static bool EditarLivro(string titulo, string novoTitulo, string novoAutor, int novoAno)
    {
        var livro = BuscarLivroPorNome(titulo);
        if (livro != null)
        {
            livro.Titulo = novoTitulo;
            livro.Autor = novoAutor;
            livro.Ano = novoAno;
            return true;
        }
        return false;
    }

    public static bool ExcluirLivro(string titulo)
    {
        var livro = BuscarLivroPorNome(titulo);
        if (livro != null)
        {
            ListaDeLivros.Remove(livro);
            return true;
        }
        return false;
    }

    public static void SalvarEmArquivo()
    {
        var json = JsonSerializer.Serialize(ListaDeLivros, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, json);
    }

    public static void CarregarDeArquivo()
    {
        if (File.Exists(CaminhoArquivo))
        {
            string json = File.ReadAllText(CaminhoArquivo);
            ListaDeLivros = JsonSerializer.Deserialize<List<Livros>>(json) ?? new List<Livros>();
        }
    }
}
