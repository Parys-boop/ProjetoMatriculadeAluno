using System.Collections.Generic;

public class MatriculaPorAlunoViewModel
{
    public Aluno Aluno { get; set; } = new Aluno();
    public List<Matricula> Matriculas { get; set; } = new List<Matricula>();
    public List<Curso> CursosDisponiveis { get; set; } = new List<Curso>();
}