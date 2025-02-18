
namespace Business.Models;

public class CompanyServiceModel
{
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public int UnitId { get; set; }
    public int CurrencyId { get; set; }
}
