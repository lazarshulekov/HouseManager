using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    using DAL.Models;

    public interface IBuildingService
    {
        Task AddAsync(Building bld);

        Task DeleteAsync(int buildingId);

        List<Building> GetAllBuildings();

        Task<Building> GetBuildingByIdAsync(int buildingId);

        Task UpdateAsync(Building bld);
    }
}