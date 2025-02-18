

using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ITimeFrameService
{
    Task<bool> CreateTimeframe(TimeframeModel timeframe);
    Task<IEnumerable<TimeframeEntity>> GetAllTimeframes();
    Task<TimeframeEntity> UpdateTimeframe(int id, TimeframeEntity timeframe);
    Task<bool> DeleteTimeframe(int id);
}
