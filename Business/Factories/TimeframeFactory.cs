

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class TimeframeFactory
{
    public static TimeframeEntity Create(TimeframeModel timeframe)
    {
        return new TimeframeEntity
        {
            StartDate = timeframe.StartDate,
            EndDate = timeframe.EndDate,
        };
    }
}
