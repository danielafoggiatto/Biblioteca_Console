using Biblioteca.ConsoleUI;
using Biblioteca.Domain;

public class CadastroLeitor
{
    public void AddLeitor()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Tipo de usu�rio (Aluno/Professor): ");
        string tipo = Console.ReadLine();

        int matricula = Program.LerInteiro("Matr�cula: ");
        if (Leitor.BuscarLeitorPorMatricula(matricula) != null)
        {
            Console.WriteLine("J� existe um leitor com essa matr�cula.");
            return;
        }


        var leitor = new Leitor(nome, tipo, matricula);
        Leitor.ListaLeitores.Add(leitor);

        Leitor.SalvarEmArquivo(); 
        Console.WriteLine("Leitor cadastrado com sucesso!");
    }

}