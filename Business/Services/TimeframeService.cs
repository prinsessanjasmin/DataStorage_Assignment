

using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class TimeframeService(TimeframeRepository timeframeRepository) : ITimeFrameService
{
    private readonly ITimeframeRepository _timeframeRepository = timeframeRepository;

    public async Task<bool> CreateTimeframe(TimeframeModel timeframe)
    {
        try
        {
            TimeframeEntity timeframeEntity = TimeframeFactory.Create(timeframe);

            await _timeframeRepository.CreateAsync(timeframeEntity);
            await _timeframeRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating timeframe :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<TimeframeEntity>> GetAllTimeframes()
    {
        IEnumerable<TimeframeEntity> list = [];

        try
        {
            list = await _timeframeRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding timeframes :: {ex.Message}");
            return null!;
        }
    }

    public async Task<TimeframeEntity> UpdateTimeframe(int id, TimeframeEntity timeframe)
    {
        try
        {
            TimeframeEntity existingTimeframe = await _timeframeRepository.UpdateAsync(x => x.Id == id, timeframe);
            return existingTimeframe;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating timeframe :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteTimeframe(int id)
    {
        try
        {
            bool deleted = await _timeframeRepository.DeleteAsync(x => x.Id == id);
            return deleted;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting timeframe :: {ex.Message}");
            return false!;
        }
    }
}
