

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Data.Entities;

public class CompanyServiceEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Suggestion from Claude AI
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    public int UnitId { get; set; }
    public UnitEntity Unit { get; set; } = null!; 

    [Required]
    public int CurrencyId { get; set; }
    public CurrencyEntity Currency { get; set; } = null!;
}
