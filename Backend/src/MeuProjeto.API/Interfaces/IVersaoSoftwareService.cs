using MeuProjeto.API.Models;

namespace MeuProjeto.API.Interfaces;

public interface IVersaoSoftwareService
{
    Task<IEnumerable<VersaoSoftware>> ListarPorSoftwareAsync(
        int softwareId);

    Task<VersaoSoftware?> BuscarPorIdAsync(int id);

    Task<VersaoSoftware> CriarAsync(VersaoSoftware versao);

    Task<bool> AtualizarAsync(int id, VersaoSoftware versao);

    Task<bool> ExcluirAsync(int id);
}