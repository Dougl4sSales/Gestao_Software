using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.API.Models;

public class Software
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do software é obrigatório.")]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;
    [StringLength(500)]
    public string? Descricao { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    // Um software pode possuir várias versões.
    public ICollection<VersaoSoftware> Versoes { get; set; }
        = new List<VersaoSoftware>();
}