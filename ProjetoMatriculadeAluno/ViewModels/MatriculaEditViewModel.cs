using System.ComponentModel.DataAnnotations;

public class MatriculaEditViewModel
{
    public int AlunoId { get; set; }
    public int CursoId { get; set; }
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "O status é obrigatório")]
    [Display(Name = "Status")]
    public StatusMatricula Status { get; set; }

    [Required(ErrorMessage = "O progresso é obrigatório")]
    [Range(0, 100, ErrorMessage = "O progresso deve ser entre 0 e 100")]
    [Display(Name = "Progresso (%)")]
    public int Progresso { get; set; }

    [Required(ErrorMessage = "O preço pago é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    [Display(Name = "Preço Pago")]
    public decimal PrecoPago { get; set; }

    [Range(0, 10, ErrorMessage = "A nota deve ser entre 0 e 10")]
    [Display(Name = "Nota Final")]
    public int? NotaFinal { get; set; }

    // Para exibir na view
    public string? AlunoNome { get; set; }
    public string? CursoTitulo { get; set; }
}