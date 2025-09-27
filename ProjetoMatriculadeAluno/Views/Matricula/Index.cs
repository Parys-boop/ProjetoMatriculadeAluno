using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Threading;
using System;

@model IEnumerable<Matricula>
@{
    ViewData["Title"] = "Matrículas";
}

< div class= "container mt-4" >
    < h2 class= "mb-3" >
        < i class= "fas fa-graduation-cap" ></ i > Matrículas
    </ h2 >


    < div class= "row mb-3" >
        < div class= "col" >
            < a href = "@Url.Action("Create")" class= "btn btn-success" >
                < i class= "fas fa-plus" ></ i > Nova Matrícula
            </ a >
        </ div >
    </ div >

    @if(TempData["Alert"] != null)
    {
        < div class= "alert alert-success alert-dismissible fade show" role = "alert" >
            < i class= "fas fa-check-circle" ></ i > @TempData["Alert"]
            < button type = "button" class= "btn-close" data - bs - dismiss = "alert" ></ button >
        </ div >
    }


    < div class= "card shadow-sm" >
        < div class= "card-body p-0" >
            < table class= "table table-striped table-hover mb-0" >
                < thead class= "table-dark" >
                    < tr >
                        < th > Aluno </ th >
                        < th > Curso </ th >
                        < th > Data da Matrícula</th>
                        <th>Status</th>
                        <th>Progresso</th>
                        <th>Preço Pago</th>
                        <th>Nota Final</th>
                        <th width = "150" > Ações </ th >
                    </ tr >
                </ thead >
                < tbody >
                    @if(Model.Any())
                    {
                        @foreach (var matricula in Model)
{
                            < tr >
                                < td > @matricula.Aluno?.Nome </ td >
                                < td > @matricula.Curso?.Titulo </ td >
                                < td > @matricula.Data.ToString("dd/MM/yyyy") </ td >
                                < td >
                                    @switch(matricula.Status)
                                    {
                                        case StatusMatricula.Ativo:
                                            < span class= "badge bg-success" > Ativo </ span >
                                            break;
                                        case StatusMatricula.Concluido:
                                            < span class= "badge bg-primary" > Concluído </ span >
                                            break;
                                        case StatusMatricula.Cancelado:
                                            < span class= "badge bg-danger" > Cancelado </ span >
                                            break;
                                    }
                                </ td >
                                < td >
                                    < div class= "progress" style = "height: 20px;" >
                                        < div class= "progress-bar @(matricula.Progresso == 100 ? "bg - success" : "bg - info")"
                                             role = "progressbar"
                                             style = "width: @matricula.Progresso%" >
                                            @matricula.Progresso %
                                        </ div >
                                    </ div >
                                </ td >
                                < td > @matricula.PrecoPago.ToString("C") </ td >
                                < td >
                                    @if(matricula.NotaFinal.HasValue)
                                    {
                                        < span class= "badge bg-info" > @matricula.NotaFinal.Value </ span >
                                    }
                                    else
{
                                        < span class= "text-muted" > -</ span >
                                    }
                                </ td >
                                < td >
                                    < div class= "btn-group" role = "group" >
                                        < a href = "@Url.Action("Edit", new { alunoId = matricula.AlunoId, cursoId = matricula.CursoId })" 
                                           class= "btn btn-sm btn-warning" title = "Editar" >
                                            < i class= "fas fa-edit" ></ i >
                                        </ a >
                                        < a href = "@Url.Action("Delete", new { alunoId = matricula.AlunoId, cursoId = matricula.CursoId })" 
                                           class= "btn btn-sm btn-danger" title = "Cancelar" >
                                            < i class= "fas fa-times" ></ i >
                                        </ a >
                                    </ div >
                                </ td >
                            </ tr >
                        }
                    }
                    else
{
                        < tr >
                            < td colspan = "8" class= "text-center py-4" >
                                < i class= "fas fa-graduation-cap fa-2x text-muted" ></ i >
                                < p class= "mt-2 text-muted" > Nenhuma matrícula encontrada.</ p >
                                < a href = "@Url.Action("Create")" class= "btn btn-success" >
                                    < i class= "fas fa-plus" ></ i > Criar Primeira Matrícula
                                </a>
                            </td>
                        </tr>
                    }
                </ tbody >
            </ table >
        </ div >
    </ div >
</ div >