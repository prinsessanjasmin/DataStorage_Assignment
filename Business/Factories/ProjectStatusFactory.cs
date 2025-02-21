

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectStatusFactory
{
    public static ProjectStatusEntity Create(string projectStatus)
    {
        return new ProjectStatusEntity
        {
            StatusName = projectStatus,
        };
    }

    public static ProjectStatusEntity Create(ProjectStatusModel projectStatus)
    {
        return new ProjectStatusEntity
        {
            StatusName = projectStatus.StatusName,
        };
    }

}
