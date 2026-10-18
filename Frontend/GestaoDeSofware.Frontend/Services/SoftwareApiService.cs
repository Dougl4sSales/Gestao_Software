using System.Net.Http;
using System.Net.Http.Json;
using GestaoDeSofware.Frontend.DTOs;

namespace GestaoDeSofware.Frontend.Services;

public class SoftwareApiService
{
    private readonly HttpClient _httpClient;

    public SoftwareApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SoftwareDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var softwares = await _httpClient.GetFromJsonAsync<List<SoftwareDto>>("api/softwares", cancellationToken);

        return softwares ?? new List<SoftwareDto>();
    }
    public async Task CriarAsync(SoftwareDto software)
    {
        var response = await _httpClient.PostAsJsonAsync("api/softwares", software);        
    }

    //CRIAR VERSOES
    public async Task<SoftwareDto> BuscarPorIdAsync(int id)
    {
        var software = await _httpClient.GetFromJsonAsync<SoftwareDto>(
            $"api/softwares/{id}");

        return software
            ?? throw new HttpRequestException("Software não encontrado.");
    }

    public async Task<VersaoSoftwareDto> BuscarVersaoAsync(int id)
    {
        var versao = await _httpClient.GetFromJsonAsync<VersaoSoftwareDto>(
            $"api/versoes/{id}");

        return versao
            ?? throw new HttpRequestException("Versão não encontrada.");
    }

    public async Task CriarVersaoAsync(VersaoSoftwareDto versao)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"api/softwares/{versao.SoftwareId}/versoes",
            versao
        );

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();

            throw new HttpRequestException(
                $"Erro ao cadastrar versão. " +
                $"Status: {(int)response.StatusCode} ({response.StatusCode}). " +
                $"Detalhes: {erro}"
            );
        }
    }

    public async Task AtualizarVersaoAsync(VersaoSoftwareDto versao)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"api/versoes/{versao.Id}", versao);

        response.EnsureSuccessStatusCode();
    }
}