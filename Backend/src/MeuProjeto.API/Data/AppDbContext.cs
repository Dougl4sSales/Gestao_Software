
using Microsoft.EntityFrameworkCore;
using MeuProjeto.API.Models;

namespace MeuProjeto.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Software> Softwares => Set<Software>();

    public DbSet<VersaoSoftware> VersoesSoftware =>
        Set<VersaoSoftware>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(s => s.Descricao)
                .HasMaxLength(500);

            entity.Property(s => s.DataCadastro)
                .IsRequired();
        });

        modelBuilder.Entity<VersaoSoftware>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.NumeroVersao)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(v => v.DataRelease)
                .IsRequired();

            entity.Property(v => v.Depreciado)
                .HasDefaultValue(false);

            entity.HasOne(v => v.Software)
                .WithMany(s => s.Versoes)
                .HasForeignKey(v => v.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
