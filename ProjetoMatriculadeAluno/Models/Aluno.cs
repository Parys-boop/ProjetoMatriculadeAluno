using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Aluno
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}