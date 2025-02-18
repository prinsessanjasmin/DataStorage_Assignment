

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class TimeframeModel
{
    [DataType(DataType.Date)]    // Claude AI
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Claude AI
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]    // Claude AI
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Claude AI
    public DateTime EndDate { get; set; }
}
