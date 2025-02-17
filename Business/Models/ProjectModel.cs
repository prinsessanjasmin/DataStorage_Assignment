
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Business.Models;

public class ProjectModel
{
    public string Title { get; set; } = null!;
    public string? About { get; set; }
    public int? HoursWorked { get; set; }
    public decimal? TotalPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ProjectStatusId { get; set; }
    public int CustomerId { get; set; }
    public int ProjectManagerId { get; set; } 
    public int CompanyServiceId { get; set; } 

}
