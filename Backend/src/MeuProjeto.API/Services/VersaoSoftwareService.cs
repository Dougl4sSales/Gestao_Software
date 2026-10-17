using Microsoft.EntityFrameworkCore;
using MeuProjeto.API.Data;
using MeuProjeto.API.Interfaces;
using MeuProjeto.API.Models;

namespace MeuProjeto.API.Services;

public class VersaoSoftwareService : IVersaoSoftwareService
{
    private readonly AppDbContext _context;

    public VersaoSoftwareService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VersaoSoftware>>
        ListarPorSoftwareAsync(int softwareId)
    {
        return await _context.VersoesSoftware
            .AsNoTracking()
            .Where(v => v.SoftwareId == softwareId)
            .OrderByDescending(v => v.DataRelease)
            .ToListAsync();
    }

    public async Task<VersaoSoftware?> BuscarPorIdAsync(int id)
    {
        return await _context.VersoesSoftware
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<VersaoSoftware> CriarAsync(
        VersaoSoftware versao)
    {
        versao.Id = 0;
        versao.Software = null;

        _context.VersoesSoftware.Add(versao);
        await _context.SaveChangesAsync();

        return versao;
    }

    public async Task<bool> AtualizarAsync(
        int id, VersaoSoftware versao)
    {
        var existente = await _context.VersoesSoftware
            .FindAsync(id);

        if (existente is null)
            return false;

        existente.NumeroVersao = versao.NumeroVersao;
        existente.DataRelease = versao.DataRelease;
        existente.Depreciado = versao.Depreciado;
        existente.SoftwareId = versao.SoftwareId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var versao = await _context.VersoesSoftware
            .FindAsync(id);

        if (versao is null)
            return false;

        _context.VersoesSoftware.Remove(versao);
        await _context.SaveChangesAsync();

        return true;
    }
}