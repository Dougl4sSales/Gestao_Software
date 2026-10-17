using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuProjeto.API.Data;
using MeuProjeto.API.Interfaces;
using MeuProjeto.API.Models;

namespace MeuProjeto.API.Controllers;

[ApiController]
[Route("api")]
public class VersoesController : ControllerBase
{
    private readonly IVersaoSoftwareService _service;
    private readonly AppDbContext _context;

    public VersoesController(
        IVersaoSoftwareService service,
        AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    // GET /api/softwares/1/versoes
    [HttpGet("softwares/{softwareId:int}/versoes")]
    public async Task<ActionResult<IEnumerable<VersaoSoftware>>>
        ListarPorSoftware(int softwareId)
    {
        if (!await _context.Softwares.AnyAsync(s => s.Id == softwareId))
            return NotFound(new { mensagem = "Software não encontrado." });

        var versoes = await _service.ListarPorSoftwareAsync(softwareId);

        return Ok(versoes);
    }

    // GET /api/versoes/1
    [HttpGet("versoes/{id:int}")]
    public async Task<ActionResult<VersaoSoftware>> BuscarPorId(int id)
    {
        var versao = await _service.BuscarPorIdAsync(id);

        if (versao is null)
            return NotFound(new { mensagem = "Versão não encontrada." });

        return Ok(versao);
    }

    // POST /api/softwares/1/versoes
    [HttpPost("softwares/{softwareId:int}/versoes")]
    public async Task<ActionResult<VersaoSoftware>> Criar(
        int softwareId,
        [FromBody] VersaoSoftware versao)
    {
        var softwareExiste = await _context.Softwares
            .AnyAsync(s => s.Id == softwareId);

        if (!softwareExiste)
            return NotFound(new { mensagem = "Software não encontrado." });

        versao.SoftwareId = softwareId;

        var criada = await _service.CriarAsync(versao);

        return CreatedAtAction(
            nameof(BuscarPorId),
            new { id = criada.Id },
            criada);
    }

    // PUT /api/versoes/1
    [HttpPut("versoes/{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] VersaoSoftware versao)
    {
        var softwareExiste = await _context.Softwares
            .AnyAsync(s => s.Id == versao.SoftwareId);

        if (!softwareExiste)
            return BadRequest(new { mensagem = "Software informado não existe." });

        var atualizado = await _service.AtualizarAsync(id, versao);

        if (!atualizado)
            return NotFound(new { mensagem = "Versão não encontrada." });

        return NoContent();
    }

    // DELETE /api/versoes/1
    [HttpDelete("versoes/{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await _service.ExcluirAsync(id);

        if (!excluido)
            return NotFound(new { mensagem = "Versão não encontrada." });

        return NoContent();
    }
}