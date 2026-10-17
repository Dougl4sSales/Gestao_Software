using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.API.Models;

public class VersaoSoftware
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O número da versão é obrigatório.")]
    [StringLength(50)]
    public string NumeroVersao { get; set; } = string.Empty;

    [Required]
    public DateTime DataRelease { get; set; }

    public bool Depreciado { get; set; } = false;

    // Chave estrangeira para o software.
    [Required]
    public int SoftwareId { get; set; }

    public Software? Software { get; set; }
}