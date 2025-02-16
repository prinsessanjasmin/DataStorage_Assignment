

using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class TimeframeRepository(DataContext context) : BaseRepository<TimeframeEntity>(context), ITimeframeRepository
{
    private readonly DataContext _context = context;
}
