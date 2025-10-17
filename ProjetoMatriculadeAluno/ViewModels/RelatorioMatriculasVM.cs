namespace ProjetoMatriculadeAluno.ViewModels;

public class CursoMatriculaReportRow
{
    public int CursoId { get; set; }
    public string NomeCurso { get; set; } = "";
    public int QuantidadeAlunos { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal ValorMedioPorAluno => QuantidadeAlunos == 0 ? 0 : ValorTotal / QuantidadeAlunos;
}

public class RelatorioMatriculasVM
{
    public string? FiltroTitulo { get; set; }
    public List<CursoMatriculaReportRow> Linhas { get; set; } = new();

    public int TotalAlunos => Linhas.Sum(l => l.QuantidadeAlunos);
    public decimal ValorTotalArrecadado => Linhas.Sum(l => l.ValorTotal);
    public decimal ValorMedioGeral => TotalAlunos == 0 ? 0 : ValorTotalArrecadado / TotalAlunos;

    public string NomeTitulo =>
        (!string.IsNullOrWhiteSpace(FiltroTitulo))
        ? $"Relatório de Matrículas - {FiltroTitulo}"
        : "Relatório de Matrículas - Todos os Cursos";
}