
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CompanyRoleEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Suggestion from Claude AI
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string CompanyRole { get; set; } = null!;
}
