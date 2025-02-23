﻿

using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class TimeframeService(ITimeframeRepository timeframeRepository) : ITimeframeService
{
    private readonly ITimeframeRepository _timeframeRepository = timeframeRepository;

    public string ErrorMessage => throw new NotImplementedException();

    public async Task<TimeframeEntity> CreateTimeframe(TimeframeModel timeframe)
    {
        await _timeframeRepository.BeginTransactionAsync();
        try
        {
            TimeframeEntity timeframeEntity = TimeframeFactory.Create(timeframe);

            await _timeframeRepository.CreateAsync(timeframeEntity);
            await _timeframeRepository.SaveAsync();
            await _timeframeRepository.CommitTransactionAsync();
            return timeframeEntity;
        }
        catch (Exception ex)
        {
            await _timeframeRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating timeframe :: {ex.Message}");
            return null!;
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

    public async Task<TimeframeEntity> GetTimeframeById(int id)
    {
        try
        {
            TimeframeEntity timeframe = await _timeframeRepository.GetAsync(x => x.Id == id);
            if (timeframe == null)
                return null!;

            return timeframe;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding timeframe :: {ex.Message}");
            return null!;
        }
    }
    public async Task<TimeframeEntity> UpdateTimeframe(int id, TimeframeEntity timeframe)
    {
        await _timeframeRepository.BeginTransactionAsync();

        try
        {
            TimeframeEntity existingTimeframe = await _timeframeRepository.UpdateAsync(x => x.Id == id, timeframe);
            await _timeframeRepository.SaveAsync();
            await _timeframeRepository.CommitTransactionAsync();
            return existingTimeframe;
        }
        catch (Exception ex)
        {
            await _timeframeRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating timeframe :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteTimeframe(int id)
    {
        await _timeframeRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _timeframeRepository.DeleteAsync(x => x.Id == id);
            await _timeframeRepository.SaveAsync();
            await _timeframeRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _timeframeRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting timeframe :: {ex.Message}");
            return false!;
        }
    }
}
