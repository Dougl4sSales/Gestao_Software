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
        return await _context.Softwares
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Software?> BuscarPorIdAsync(int id)
    {
        return await _context.Softwares
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Software> CriarAsync(Software software)
    {
        software.Id = 0;
        software.DataCadastro = DateTime.UtcNow;

        _context.Softwares.Add(software);
        await _context.SaveChangesAsync();

        return software;
    }

    public async Task<bool> AtualizarAsync(
        int id, Software software)
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