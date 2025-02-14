using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectStatusEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Suggestion from Claude AI
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string StatusName { get; set; } = null!; 
}
