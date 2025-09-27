using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum StatusMatricula
{
    Ativo,
    Concluido,
    Cancelado
}

public class Matricula
{
    public int AlunoId { get; set; }
    public int CursoId { get; set; }

    [Column(TypeName = "timestamp with time zone")]
    public DateTime Data { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, double.MaxValue)]
    public decimal PrecoPago { get; set; }

    [Required]
    public StatusMatricula Status { get; set; }

    [Range(0, 100)]
    public int Progresso { get; set; }

    [Range(0, 10)]
    public int? NotaFinal { get; set; }

    public Aluno Aluno { get; set; } = null!;
    public Curso Curso { get; set; } = null!;
}