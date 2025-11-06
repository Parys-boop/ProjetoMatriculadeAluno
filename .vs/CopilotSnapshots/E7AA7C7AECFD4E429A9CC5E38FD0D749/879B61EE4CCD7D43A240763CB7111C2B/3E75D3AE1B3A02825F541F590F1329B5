using ProjetoMatriculadeAluno.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMatriculadeAluno;
using System.Text;

namespace ProjetoMatriculadeAluno.Controllers;

public class RelatoriosController : Controller
{
    private readonly AppDbContext _ctx;

    public RelatoriosController(AppDbContext ctx) => _ctx = ctx;

    // GET: /Relatorios/MatriculasPorCurso?filtroTitulo=CSharp
    public async Task<IActionResult> MatriculasPorCurso(string? filtroTitulo)
    {
        var matriculas = _ctx.Matriculas
            .Include(m => m.Curso)
            .Include(m => m.Aluno)
            .AsNoTracking()
            .AsQueryable();

        // Filtrar por título do curso se fornecido
        if (!string.IsNullOrWhiteSpace(filtroTitulo))
        {
            matriculas = matriculas.Where(m => m.Curso.Titulo.Contains(filtroTitulo));
        }

        // Agrupar por curso e calcular totais
        var linhas = await matriculas
            .GroupBy(m => new { m.CursoId, m.Curso.Titulo })
            .Select(g => new CursoMatriculaReportRow
            {
                CursoId = g.Key.CursoId,
                NomeCurso = g.Key.Titulo,
                QuantidadeAlunos = g.Count(),
                ValorTotal = g.Sum(m => m.PrecoPago)
            })
            .OrderByDescending(r => r.ValorTotal)
            .ToListAsync();

        var vm = new RelatorioMatriculasVM
        {
            FiltroTitulo = filtroTitulo,
            Linhas = linhas
        };

        ViewData["Title"] = "Relatório de Matrículas por Curso";
        return View(vm);
    }

    // Exportar CSV com o mesmo filtro
    public async Task<IActionResult> MatriculasPorCursoCsv(string? filtroTitulo)
    {
        var matriculas = _ctx.Matriculas
            .Include(m => m.Curso)
            .AsNoTracking()
            .AsQueryable();

        // Filtrar por título do curso se fornecido
        if (!string.IsNullOrWhiteSpace(filtroTitulo))
        {
            matriculas = matriculas.Where(m => m.Curso.Titulo.Contains(filtroTitulo));
        }

        var linhas = await matriculas
            .GroupBy(m => new { m.CursoId, m.Curso.Titulo })
            .Select(g => new
            {
                Curso = g.Key.Titulo,
                QuantidadeAlunos = g.Count(),
                ValorTotal = g.Sum(m => m.PrecoPago),
                ValorMedio = g.Sum(m => m.PrecoPago) / g.Count()
            })
            .OrderByDescending(r => r.ValorTotal)
            .ToListAsync();

        // Gerar CSV
        var sb = new StringBuilder();
        sb.AppendLine("Curso;Quantidade de Alunos;Valor Total;Valor Médio por Aluno");
        foreach (var r in linhas)
            sb.AppendLine($"\"{r.Curso}\";{r.QuantidadeAlunos};{r.ValorTotal:0.00};{r.ValorMedio:0.00}");

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return File(bytes, "text/csv", $"matriculas_por_curso_{DateTime.Now:yyyyMMddHHmm}.csv");
    }
}