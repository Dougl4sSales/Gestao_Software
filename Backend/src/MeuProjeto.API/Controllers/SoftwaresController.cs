using Microsoft.AspNetCore.Mvc;
using MeuProjeto.API.Interfaces;
using MeuProjeto.API.Models;

namespace MeuProjeto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoftwaresController : ControllerBase
{
    private readonly ISoftwareService _service;

    public SoftwaresController(ISoftwareService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Software>>> Listar()
    {
        var softwares = await _service.ListarTodosAsync();
        return Ok(softwares);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Software>> BuscarPorId(int id)
    {
        var software = await _service.BuscarPorIdAsync(id);

        if (software is null)
            return NotFound(new { mensagem = "Software não encontrado." });

        return Ok(software);
    }

    [HttpPost]
    public async Task<ActionResult<Software>> Criar(
        [FromBody] Software software)
    {
        var criado = await _softwareService.CriarAsync(software);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = criado.Id },
            criado
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id, [FromBody] Software software)
    {
        var atualizado = await _service.AtualizarAsync(id, software);

        if (!atualizado)
            return NotFound(new { mensagem = "Software não encontrado." });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await _service.ExcluirAsync(id);

        if (!excluido)
            return NotFound(new { mensagem = "Software não encontrado." });

        return NoContent();
    }
}