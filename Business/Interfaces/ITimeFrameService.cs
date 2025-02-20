

using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ITimeFrameService
{
    Task<TimeframeEntity> CreateTimeframe(TimeframeModel timeframe);
    Task<IEnumerable<TimeframeEntity>> GetAllTimeframes();
    Task<TimeframeEntity> GetTimeframeById(int id);
    Task<TimeframeEntity> UpdateTimeframe(int id, TimeframeEntity timeframe);
    Task<bool> DeleteTimeframe(int id);
}
