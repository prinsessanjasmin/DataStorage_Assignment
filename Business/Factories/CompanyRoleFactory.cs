using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class CompanyRoleFactory
    {
        public static CompanyRoleEntity Create(CompanyRoleModel model)
        {
            return new CompanyRoleEntity
            {
                CompanyRole = model.CompanyRole
            };
        }
    }
}
