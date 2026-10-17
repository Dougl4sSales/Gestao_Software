using MeuProjeto.API.Models;

namespace MeuProjeto.API.Interfaces;

public interface ISoftwareService
{
    Task<IEnumerable<Software>> ListarTodosAsync();

    Task<Software?> BuscarPorIdAsync(int id);

    Task<Software> CriarAsync(Software software);

    Task<bool> AtualizarAsync(int id, Software software);

    Task<bool> ExcluirAsync(int id);
}