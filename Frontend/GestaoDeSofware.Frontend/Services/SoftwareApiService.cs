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

    public async Task<List<SoftwareDto>> ListarAsync(
        CancellationToken cancellationToken = default)
    {
        var softwares = await _httpClient.GetFromJsonAsync<List<SoftwareDto>>(
            "api/softwares",
            cancellationToken
        );

        return softwares ?? new List<SoftwareDto>();
    }
}