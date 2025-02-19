

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class TimeframeFactory
{
    public static TimeframeEntity Create(DateTime startDate, DateTime endDate)
    {
        return new TimeframeEntity
        {
            StartDate = startDate,
            EndDate = endDate,
        };
    }

    public static TimeframeEntity Create(TimeframeModel timeframeModel)
    {
        return new TimeframeEntity
        {
            StartDate = timeframeModel.StartDate,
            EndDate = timeframeModel.EndDate,
        };
    }
}
