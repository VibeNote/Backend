using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Abstractions.Models.Shared;

public class PaginationModel
{
    private const int MaxPage = int.MaxValue;
    private const int MaxPageSize = 1000;

    [Display(Name = "Номер страницы")]
    [FromQuery(Name = "page")]
    public int? Page { get; set; }

    [Display(Name = "Количество записей на странице")]
    [FromQuery(Name = "page-size")]
    public int? PageSize { get; set; }
}