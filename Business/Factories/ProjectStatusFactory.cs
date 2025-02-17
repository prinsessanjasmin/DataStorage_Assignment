

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
}
