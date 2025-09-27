using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Curso
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoBase { get; set; }

    public int CargaHoraria { get; set; }

    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}