using Biblioteca.ConsoleUI;
using Biblioteca.Services;

public class Usuario
{
	public string Nome { get; set; }
	public string TipoUsuario { get; set; }
	public Usuario() { }

	public Usuario(string nome, string tipoUsuario)
	{
		Nome = nome;
		TipoUsuario = tipoUsuario;
	}

	public virtual void AcessarSistema()
	{
		Console.WriteLine("Usuário acessou o sistema.");
	}
}