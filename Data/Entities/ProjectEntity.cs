

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Suggestion from Claude AI
    public int Id { get; set; } 

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Title { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? About { get; set; }

    public int? HoursWorked { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? TotalPrice { get; set; }

    [Required]
    public int TimeframeId { get; set; }

    public TimeframeEntity Timeframe { get; set; } = null!;

    [Required]
    public int ProjectStatusId { get; set; }
    public ProjectStatusEntity ProjectStatus { get; set; } = null!;
    [Required]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!; 

    [Required]
    public int ProjectManagerId { get; set; }
    public EmployeeEntity ProjectManager{ get; set; } = null!; 

    [Required]
    public int CompanyServiceId { get; set; }
    public CompanyServiceEntity CompanyService { get; set; } = null!; 
}
