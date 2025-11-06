using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMatriculadeAluno.Models;

namespace ProjetoMatriculadeAluno.Controllers;

public class AuthController : Controller
{
    private readonly AppDbContext _ctx;

    public AuthController(AppDbContext ctx) => _ctx = ctx;

    // GET: /Auth/Login
    public IActionResult Login()
    {
        // Se já estiver logado, redireciona para Home
        if (HttpContext.Session.GetString("UsuarioId") != null)
        {
      return RedirectToAction("Index", "Home");
    }
  return View();
    }

    // POST: /Auth/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
 ModelState.AddModelError("", "Email e senha são obrigatórios.");
  return View();
        }

        var usuario = await _ctx.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

        if (usuario == null)
      {
  ModelState.AddModelError("", "Email ou senha inválidos.");
            return View();
        }

        // Salvar informações na sessão
        HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
     HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
        HttpContext.Session.SetString("UsuarioTipo", usuario.Tipo);

 return RedirectToAction("Index", "Home");
    }

    // GET: /Auth/Cadastro
    public IActionResult Cadastro()
 {
        // Se já estiver logado, redireciona para Home
      if (HttpContext.Session.GetString("UsuarioId") != null)
        {
      return RedirectToAction("Index", "Home");
        }
        return View();
    }

    // POST: /Auth/Cadastro
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastro(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
        return View(usuario);
        }

        // Verificar se o email já existe
        var emailExiste = await _ctx.Usuarios.AnyAsync(u => u.Email == usuario.Email);
        if (emailExiste)
        {
      ModelState.AddModelError("Email", "Este email já está cadastrado.");
        return View(usuario);
      }

_ctx.Usuarios.Add(usuario);
     await _ctx.SaveChangesAsync();

        TempData["Sucesso"] = "Cadastro realizado com sucesso! Faça login para continuar.";
      return RedirectToAction("Login");
    }

    // GET: /Auth/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
 return RedirectToAction("Login");
    }
}
