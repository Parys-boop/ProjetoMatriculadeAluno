# \# 📚 Plataforma EAD - Sistema de Matrículas

# 

# Sistema completo de gerenciamento de matrículas para plataforma EAD, desenvolvido em \*\*ASP.NET Core MVC\*\* com \*\*Entity Framework Core\*\* e \*\*PostgreSQL\*\*.

# 

# \## 🎯 Sobre o Projeto

# 

# Sistema que permite o gerenciamento completo de alunos, cursos e matrículas em uma plataforma de ensino à distância. Implementa relacionamento \*\*muitos-para-muitos (N:N)\*\* entre Alunos e Cursos através da entidade Matrícula, permitindo rastreamento detalhado do progresso educacional.

# 

# \## ✨ Funcionalidades

# 

# \### 👥 Gestão de Alunos

# \- ✅ Cadastro, edição e exclusão de alunos

# \- ✅ Busca por nome ou email

# \- ✅ Visualização de matrículas por aluno

# \- ✅ Validações de dados obrigatórios

# 

# \### 📖 Gestão de Cursos

# \- ✅ Cadastro, edição e exclusão de cursos

# \- ✅ Busca por título ou descrição

# \- ✅ Controle de preço base e carga horária

# \- ✅ Visualização de alunos matriculados

# 

# \### 🎓 Gestão de Matrículas

# \- ✅ Sistema de matrícula aluno-curso

# \- ✅ Controle de progresso (0-100%)

# \- ✅ Gestão de status (Ativo/Concluído/Cancelado)

# \- ✅ Registro de preço pago e nota final

# \- ✅ Validação de regras de negócio

# 

# \## 🛠 Tecnologias Utilizadas

# 

# \- \*\*Backend:\*\* ASP.NET Core 8.0 MVC

# \- \*\*ORM:\*\* Entity Framework Core

# \- \*\*Banco de Dados:\*\* PostgreSQL

# \- \*\*Frontend:\*\* Bootstrap 5, HTML5, CSS3

# \- \*\*Ícones:\*\* Font Awesome

# \- \*\*Validação:\*\* Client-side e Server-side

# \- \*\*IDE:\*\* Visual Studio 2022

# 

# \## 🏗 Arquitetura

# 

\### Modelo de Dados

📊 Aluno (1:N Matrícula) ├── Id (PK) ├── Nome (Required) ├── Email └── Telefone
===

# 

# 📚 Curso (1:N Matrícula) ├── Id (PK) ├── Titulo (Required) ├── Descricao ├── PrecoBase (Decimal 18,2) └── CargaHoraria (Int)

# 

🎓 Matrícula (N:N entre Aluno e Curso) ├── AlunoId (PK, FK) ├── CursoId (PK, FK) ├── Data (Timestamp UTC) ├── PrecoPago (Decimal 18,2) ├── Status (Enum) ├── Progresso (0-100) └── NotaFinal (0-10, Nullable)

###📁 Estrutura do Projeto
ProjetoMatriculadeAluno/
===

# ├── Controllers/

# │   ├── HomeController.cs

# │   ├── AlunoController.cs

# │   ├── CursoController.cs

# │   └── MatriculaController.cs

# ├── Models/

# │   ├── Aluno.cs

# │   ├── Curso.cs

# │   ├── Matricula.cs

# │   ├── AppDbContext.cs

# │   └── ViewModels/

# ├── Views/

# │   ├── Home/

# │   ├── Aluno/

# │   ├── Curso/

# │   ├── Matricula/

# │   └── Shared/

# └── wwwroot/

# &nbsp;   ├── css/

# &nbsp;   ├── js/

# &nbsp;   └── lib/

