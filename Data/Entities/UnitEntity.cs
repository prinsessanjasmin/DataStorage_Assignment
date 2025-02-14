
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UnitEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Suggestion from Claude AI
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string UnitName { get; set; } = null!;
}
