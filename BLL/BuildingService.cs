using System.Collections.Generic;

namespace BLL
{
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    using Persistence.Models;

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

        public async Task AddAsync(BuildingViewModel bldVm)
        {
            var bhm = bldVm.SelectedManagers.Select(
                b => new BuildingHousemanagers() { BuildingId = bldVm.Id, HouseManagerId = b });

            var bld = new Building()
                          {
                              Id = bldVm.Id,
                              City = bldVm.City,
                              Number = bldVm.Number,
                              Street = bldVm.Street,
                              BuildingHouseManagers = bhm.ToList(),
            };

            context.Buildings.Add(bld);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BuildingViewModel bldVm)
        {
            var allManagers = await userService.GetHouseManagersAsync();
            var bhm = bldVm.SelectedManagers.Select(
                b => new BuildingHousemanagers() { BuildingId = bldVm.Id, HouseManagerId = b });

            var bhmEf = context.BuildingHousemanagers.Where(x => x.BuildingId == bldVm.Id);

            context.BuildingHousemanagers.RemoveRange(bhmEf);
            await context.SaveChangesAsync();

            var bld = await context.Buildings.FindAsync(bldVm.Id);
            bld.City = bldVm.City;
            bld.Id = bldVm.Id;
            bld.Number = bldVm.Number;
            bld.Street = bldVm.Street;
                                   //BuildingProperties = currentBuilding.BuildingProperties,
            bld.BuildingHouseManagers = bhm.ToList();

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
