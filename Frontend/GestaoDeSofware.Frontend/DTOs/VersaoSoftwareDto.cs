namespace GestaoDeSofware.Frontend.DTOs;

public class VersaoSoftwareDto
{
    public int Id { get; set; }
    public string NumeroVersao { get; set; } = string.Empty;
    public DateTime DataRelease { get; set; }
    public bool Depreciado { get; set; }
    public int SoftwareId { get; set; }
}