using Biblioteca.ConsoleUI;
using Biblioteca.Domain;

public class CadastroLivro
{
    public CadastroLivro() { }
    public void AddLivro()
    {
        Console.Write("Nome do Livro: ");
        string titulo = Console.ReadLine();
        if (Livros.BuscarLivroPorNome(titulo) != null)
        {
            Console.WriteLine("Livro já cadastrado.");
            return;
        }


        Console.Write("Autor: ");
        string autor = Console.ReadLine();


        int ano = Program.LerInteiro("Ano: ");

        var livro = new Livros(titulo, autor, ano);
        Livros.ListaDeLivros.Add(livro);

        Livros.SalvarEmArquivo();
        Console.WriteLine("Livro cadastrado com sucesso!");
    }
}