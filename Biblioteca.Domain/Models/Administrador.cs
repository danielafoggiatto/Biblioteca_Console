using Biblioteca.ConsoleUI;
using Biblioteca.Services;

public class Administrador : Usuario
{
    public int Login { get; set; }
    public Administrador(string nome, string tipoUsuario, int login) : base(nome, tipoUsuario) { Login = login; }

    public override void AcessarSistema()
    {
        Console.WriteLine("Administrador acessou o painel de controle.");
    }

}