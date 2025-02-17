

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(ProjectModel projectModel)
    {
        return new ProjectEntity
        {
            Title = projectModel.Title,
            About = projectModel.About,
            HoursWorked = projectModel.HoursWorked,
            TotalPrice = projectModel.TotalPrice,
            Timeframe = TimeframeFactory.Create(projectModel.StartDate, projectModel.EndDate),
            ProjectStatusId = projectModel.ProjectStatusId,
            CustomerId = projectModel.CustomerId,
            ProjectManagerId = projectModel.ProjectManagerId,
            CompanyServiceId = projectModel.CompanyServiceId
        };
    }
}
