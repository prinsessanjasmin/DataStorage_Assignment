

using Business.Models;
using Business.Services;
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
            Quantity = projectModel.Quantity,
            Timeframe = TimeframeFactory.Create(projectModel.StartDate, projectModel.EndDate),
            ProjectStatusId = projectModel.ProjectStatusId,
            CustomerId = projectModel.CustomerId,
            ProjectManagerId = projectModel.ProjectManagerId,
            CompanyServiceId = projectModel.CompanyServiceId
        };
    }

    public static ProjectEntity Create(ProjectModel projectModel, decimal totalPrice)
    {
        return new ProjectEntity
        {
            Title = projectModel.Title,
            About = projectModel.About,
            Quantity = projectModel.Quantity,
            TotalPrice = totalPrice,
            Timeframe = TimeframeFactory.Create(projectModel.StartDate, projectModel.EndDate),
            ProjectStatusId = projectModel.ProjectStatusId,
            CustomerId = projectModel.CustomerId,
            ProjectManagerId = projectModel.ProjectManagerId,
            CompanyServiceId = projectModel.CompanyServiceId
        };
    }
}
