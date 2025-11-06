using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CursoController : Controller
{
    private readonly AppDbContext _context;
    public CursoController(AppDbContext context) { _context = context; }

    // GET: Curso
    public async Task<IActionResult> Index(string busca)
    {
        var cursos = string.IsNullOrWhiteSpace(busca)
            ? await _context.Cursos.ToListAsync()
            : await _context.Cursos.Where(c => c.Titulo.Contains(busca) ||
                                              (c.Descricao != null && c.Descricao.Contains(busca))).ToListAsync();
        return View(cursos);
    }

    // GET: Curso/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Curso/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Curso curso)
    {
        if (ModelState.IsValid)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
            TempData["Alert"] = "Curso cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        return View(curso);
    }

    // GET: Curso/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }
        return View(curso);
    }

    // POST: Curso/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Curso curso)
    {
        if (id != curso.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
                TempData["Alert"] = "Curso atualizado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(curso.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(curso);
    }

    // GET: Curso/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .Include(c => c.Matriculas)
            .ThenInclude(m => m.Aluno)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // GET: Curso/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .FirstOrDefaultAsync(m => m.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // POST: Curso/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso != null)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            TempData["Alert"] = "Curso excluído com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CursoExists(int id)
    {
        return _context.Cursos.Any(e => e.Id == id);
    }
}