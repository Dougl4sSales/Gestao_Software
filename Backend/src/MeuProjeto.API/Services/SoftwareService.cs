using Microsoft.EntityFrameworkCore;
using MeuProjeto.API.Data;
using MeuProjeto.API.Interfaces;
using MeuProjeto.API.Models;

namespace MeuProjeto.API.Services;

public class SoftwareService : ISoftwareService
{
    private readonly AppDbContext _context;

    public SoftwareService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Software>> ListarTodosAsync()
    {
        var resultado = await (
            from software in _context.Softwares.AsNoTracking()
            join versao in _context.VersoesSoftware.AsNoTracking()
                on software.Id equals versao.SoftwareId into versoes
            select new Software
            {
                Id = software.Id,
                Nome = software.Nome,
                Descricao = software.Descricao,
                DataCadastro = software.DataCadastro,
                Versoes = versoes.ToList()
            }
        ).ToListAsync();

        return resultado;
    }    
    public async Task<Software?> BuscarPorIdAsync(int id)
    {
        return await (
            from software in _context.Softwares.AsNoTracking()
            where software.Id == id
            join versao in _context.VersoesSoftware.AsNoTracking()
                on software.Id equals versao.SoftwareId into versoes
            select new Software
            {
                Id = software.Id,
                Nome = software.Nome,
                Descricao = software.Descricao,
                DataCadastro = software.DataCadastro,
                Versoes = versoes.ToList()
            }
        ).FirstOrDefaultAsync();
    }
    public async Task<Software> CriarAsync(Software software)
    {
        Console.WriteLine($"Nome recebido: {software.Nome}");
        Console.WriteLine($"Quantidade de versões: {software.Versoes?.Count ?? 0}");

        software.Id = 0;
        software.DataCadastro = DateTime.UtcNow;

        if (software.Versoes != null)
        {
            foreach (var versao in software.Versoes)
            {
                versao.Id = 0;
                versao.SoftwareId = 0;
                versao.Software = null;
            }
        }

        _context.Softwares.Add(software);
        await _context.SaveChangesAsync();

        Console.WriteLine($"Software salvo com ID: {software.Id}");

        return software;
    }

    public async Task<bool> AtualizarAsync(int id, Software software)
    {
        var existente = await _context.Softwares
            .FindAsync(id);

        if (existente is null)
            return false;

        existente.Nome = software.Nome;
        existente.Descricao = software.Descricao;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var software = await _context.Softwares
            .FindAsync(id);

        if (software is null)
            return false;

        _context.Softwares.Remove(software);
        await _context.SaveChangesAsync();

        return true;
    }
}