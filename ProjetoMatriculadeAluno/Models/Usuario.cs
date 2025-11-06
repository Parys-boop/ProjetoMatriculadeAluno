using System.ComponentModel.DataAnnotations;

namespace ProjetoMatriculadeAluno.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione o tipo de usuário")]
    public string Tipo { get; set; } = string.Empty; // "Estudante" ou "Professor"
}
