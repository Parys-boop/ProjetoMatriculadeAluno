﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

public class MatriculaController : Controller
{
    private readonly AppDbContext _context;
    public MatriculaController(AppDbContext context) { _context = context; }

    // GET: Matricula
    public async Task<IActionResult> Index(string? busca)
    {
        var matriculas = string.IsNullOrWhiteSpace(busca)
            ? await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .OrderByDescending(m => m.Data)
                .ToListAsync()
            : await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .Where(m => (m.Aluno != null && m.Aluno.Nome.Contains(busca)) ||
                           (m.Curso != null && m.Curso.Titulo.Contains(busca)))
                .OrderByDescending(m => m.Data)
                .ToListAsync();

        return View(matriculas);
    }

    // GET: Matricula/Create
    public async Task<IActionResult> Create()
    {
        ViewData["AlunoId"] = new SelectList(await _context.Alunos.ToListAsync(), "Id", "Nome");
        ViewData["CursoId"] = new SelectList(await _context.Cursos.ToListAsync(), "Id", "Titulo");
        return View(new MatriculaCreateViewModel());
    }

    // POST: Matricula/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MatriculaCreateViewModel viewModel)
    {
        // Validações personalizadas
        if (viewModel.Status == StatusMatricula.Concluido && viewModel.Progresso != 100)
        {
            ModelState.AddModelError("", "Para status Concluído, o progresso deve ser 100%.");
        }

        // Verificar se a matrícula já existe
        var matriculaExiste = await _context.Matriculas
            .AnyAsync(m => m.AlunoId == viewModel.AlunoId && m.CursoId == viewModel.CursoId);

        if (matriculaExiste)
        {
            ModelState.AddModelError("", "Este aluno já está matriculado neste curso.");
        }

        if (ModelState.IsValid)
        {
            var matricula = new Matricula
            {
                AlunoId = viewModel.AlunoId,
                CursoId = viewModel.CursoId,
                Status = viewModel.Status,
                Progresso = viewModel.Progresso,
                PrecoPago = viewModel.PrecoPago,
                NotaFinal = viewModel.NotaFinal.HasValue ? (int)viewModel.NotaFinal.Value : null,
                Data = DateTime.UtcNow
            };

            _context.Add(matricula);
            await _context.SaveChangesAsync();
            TempData["Alert"] = "Matrícula realizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // Recarregar dados em caso de erro
        ViewData["AlunoId"] = new SelectList(await _context.Alunos.ToListAsync(), "Id", "Nome", viewModel.AlunoId);
        ViewData["CursoId"] = new SelectList(await _context.Cursos.ToListAsync(), "Id", "Titulo", viewModel.CursoId);
        return View(viewModel);
    }

    // GET: Matricula/Details/5/3
    public async Task<IActionResult> Details(int alunoId, int cursoId)
    {
        var matricula = await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

        if (matricula == null)
        {
            return NotFound();
        }

        return View(matricula);
    }

    // GET: Matricula/Edit/5/3
    public async Task<IActionResult> Edit(int alunoId, int cursoId)
    {
        var matricula = await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

        if (matricula == null)
        {
            return NotFound();
        }

        var viewModel = new MatriculaEditViewModel
        {
            AlunoId = matricula.AlunoId,
            CursoId = matricula.CursoId,
            Data = matricula.Data,
            Status = matricula.Status,
            Progresso = matricula.Progresso,
            PrecoPago = matricula.PrecoPago,
            NotaFinal = matricula.NotaFinal,
            AlunoNome = matricula.Aluno?.Nome,
            CursoTitulo = matricula.Curso?.Titulo
        };

        return View(viewModel);
    }

    // POST: Matricula/Edit/5/3
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int alunoId, int cursoId, MatriculaEditViewModel viewModel)
    {
        if (alunoId != viewModel.AlunoId || cursoId != viewModel.CursoId)
        {
            return NotFound();
        }

        // Validações personalizadas
        if (viewModel.Status == StatusMatricula.Concluido && viewModel.Progresso != 100)
        {
            ModelState.AddModelError("", "Para status Concluído, o progresso deve ser 100%.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                var matricula = await _context.Matriculas
                    .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

                if (matricula == null)
                {
                    return NotFound();
                }

                // Atualizar apenas os campos editáveis
                matricula.Status = viewModel.Status;
                matricula.Progresso = viewModel.Progresso;
                matricula.PrecoPago = viewModel.PrecoPago;
                matricula.NotaFinal = viewModel.NotaFinal;

                _context.Update(matricula);
                await _context.SaveChangesAsync();
                TempData["Alert"] = "Matrícula atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(viewModel.AlunoId, viewModel.CursoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // Recarregar dados em caso de erro
        var matriculaError = await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

        if (matriculaError != null)
        {
            viewModel.AlunoNome = matriculaError.Aluno?.Nome;
            viewModel.CursoTitulo = matriculaError.Curso?.Titulo;
        }

        return View(viewModel);
    }

    // GET: Matricula/Delete/5/3
    public async Task<IActionResult> Delete(int alunoId, int cursoId)
    {
        var matricula = await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Curso)
            .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

        if (matricula == null)
        {
            return NotFound();
        }

        return View(matricula);
    }

    // POST: Matricula/Delete/5/3
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int alunoId, int cursoId)
    {
        var matricula = await _context.Matriculas
            .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

        if (matricula != null)
        {
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            TempData["Alert"] = "Matrícula cancelada com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Matricula/PorAluno/5
    public async Task<IActionResult> PorAluno(int alunoId)
    {
        var aluno = await _context.Alunos
            .Include(a => a.Matriculas)
            .ThenInclude(m => m.Curso)
            .FirstOrDefaultAsync(a => a.Id == alunoId);

        if (aluno == null)
        {
            return NotFound();
        }

        var cursosDisponiveis = await _context.Cursos
            .Where(c => !c.Matriculas.Any(m => m.AlunoId == alunoId))
            .ToListAsync();

        var viewModel = new MatriculaPorAlunoViewModel
        {
            Aluno = aluno,
            Matriculas = aluno.Matriculas.ToList(),
            CursosDisponiveis = cursosDisponiveis
        };

        return View(viewModel);
    }

    // POST: Matricula/SalvarMatricula
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SalvarMatricula(MatriculaPorAlunoViewModel viewModel)
    {
        if (viewModel.Matriculas != null)
        {
            foreach (var matricula in viewModel.Matriculas)
            {
                // Validações
                if (matricula.Status == StatusMatricula.Concluido && matricula.Progresso != 100)
                {
                    TempData["Alert"] = "Para status Concluído, o progresso deve ser 100%.";
                    return RedirectToAction("PorAluno", new { alunoId = viewModel.Aluno?.Id });
                }

                _context.Update(matricula);
            }

            await _context.SaveChangesAsync();
            TempData["Alert"] = "Matrículas salvas com sucesso!";
        }

        return RedirectToAction("PorAluno", new { alunoId = viewModel.Aluno?.Id });
    }

    private bool MatriculaExists(int alunoId, int cursoId)
    {
        return _context.Matriculas.Any(e => e.AlunoId == alunoId && e.CursoId == cursoId);
    }
}