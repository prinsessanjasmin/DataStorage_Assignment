using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class TimeframeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Suggestion from Claude AI
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]    // Claude AI
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Claude AI
    public DateTime StartDate { get; set; }

    [Required]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]    // Claude AI
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Claude AI
    public DateTime EndDate { get; set; }
}
