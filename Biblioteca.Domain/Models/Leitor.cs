using Biblioteca.ConsoleUI;
using Biblioteca.Services;


public class Leitor : Usuario
{
    public int Matricula { get; set; }
    public static List<Leitor> ListaLeitores { get; private set; } = new List<Leitor>();

    private static string CaminhoArquivo = "leitores.json";
    public Leitor() { }

    public Leitor(string nome, string tipoUsuario, int matricula) : base(nome, tipoUsuario) { Matricula = matricula; }

    public override void AcessarSistema()
    {
        Console.WriteLine("Leitor acessou a biblioteca.");
    }
    public static void ListarLeitores()
    {
        if (ListaLeitores.Count == 0)
        {
            Console.WriteLine("Nenhum leitor cadastrado.");
            return;
        }

        foreach (var leitor in ListaLeitores)
        {
            Console.WriteLine($"Nome: {leitor.Nome}, Tipo: {leitor.TipoUsuario}, Matrícula: {leitor.Matricula}");
        }
    }

    public static Leitor BuscarLeitorPorMatricula(int matricula)
    {
        return ListaLeitores.FirstOrDefault(l => l.Matricula == matricula);
    }

    public static Leitor BuscarLeitorPorNome(string nome)
    {
        return ListaLeitores.FirstOrDefault(l => l.Nome.IndexOf(nome, StringComparison.OrdinalIgnoreCase) >= 0);

    }

    public static bool EditarLeitor(int matricula, string novoNome)
    {
        var leitor = BuscarLeitorPorMatricula(matricula);
        if (leitor != null)
        {
            leitor.Nome = novoNome;
            return true;
        }
        return false;
    }

    public static bool ExcluirLeitor(int matricula)
    {
        var leitor = BuscarLeitorPorMatricula(matricula);

        if (leitor == null)
        {
            return false;
        }

        Console.Write("Tem certeza que deseja excluir? (s/n): ");
        string confirmacao = Console.ReadLine();

        if (confirmacao.ToLower() == "s")
        {
            ListaLeitores.Remove(leitor);
            return true;
        }
        else
        {
            Console.WriteLine("Exclusão cancelada.");
            return false;
        }
    }


    public static void SalvarEmArquivo()
    {
        var json = JsonSerializer.Serialize(ListaLeitores, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, json);
    }

    public static void CarregarDeArquivo()
    {
        if (File.Exists(CaminhoArquivo))
        {
            string json = File.ReadAllText(CaminhoArquivo);
            ListaLeitores = JsonSerializer.Deserialize<List<Leitor>>(json) ?? new List<Leitor>();
        }
    }
}