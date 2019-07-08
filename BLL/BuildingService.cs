using System.Collections.Generic;

namespace BLL
{
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    public class BuildingService : IBuildingService
    {
        private readonly AppDbContext context;

        private readonly IAppUserService userService;

        public BuildingService(AppDbContext context, IAppUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public List<Building> GetAllBuildings()
        {
            return context.Buildings.ToList();
        }

        public async Task AddAsync(Building building)
        {
            context.Buildings.Add(building);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Building building)
        {
            var allManagers = await userService.GetHouseManagersAsync();

            var bhmEf = context.BuildingHousemanagers.Where(x => x.BuildingId == building.Id);

            context.BuildingHousemanagers.RemoveRange(bhmEf);
            await context.SaveChangesAsync();

            var bld = await context.Buildings.FindAsync(building.Id);
            bld.City = building.City;
            bld.Id = building.Id;
            bld.Number = building.Number;
            bld.Street = building.Street;
            bld.BuildingHouseManagers = building.BuildingHouseManagers;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int bldId)
        {
            var building = await GetBuildingByIdAsync(bldId);
            context.Remove(building);
            await context.SaveChangesAsync();
        }

        public async Task<Building> GetBuildingByIdAsync(int buildingId)
        {
            return await context.Buildings.FindAsync(buildingId);
        }
    }
}
