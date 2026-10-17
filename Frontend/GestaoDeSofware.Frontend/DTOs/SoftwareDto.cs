namespace GestaoDeSofware.Frontend.DTOs;

public class SoftwareDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public List<VersaoSoftwareDto> Versoes { get; set; } = new();
}