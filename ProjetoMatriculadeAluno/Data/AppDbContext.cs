﻿using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Matricula>()
            .HasKey(m => new { m.AlunoId, m.CursoId });

        modelBuilder.Entity<Matricula>()
            .HasOne(m => m.Aluno)
            .WithMany(a => a.Matriculas)
            .HasForeignKey(m => m.AlunoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Matricula>()
            .HasOne(m => m.Curso)
            .WithMany(c => c.Matriculas)
            .HasForeignKey(m => m.CursoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Curso>()
            .Property(c => c.PrecoBase)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Matricula>()
            .Property(m => m.PrecoPago)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Matricula>()
            .Property(m => m.Data)
            .HasColumnType("timestamp with time zone"); // Utilizando UTC
    }
}