

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class TimeframeModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
