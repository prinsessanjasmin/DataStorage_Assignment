
using Data.Interfaces;
using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class CurrencyRepository(DataContext context) : BaseRepository<CurrencyEntity>(context), ICurrencyRepository
{
    private readonly DataContext _context = context;
}
