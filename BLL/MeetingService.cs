namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    using Persistence.Models;

    public class MeetingService
    {
        //private readonly IMemoryCache cache;

        private readonly AppDbContext context;

        public MeetingService(AppDbContext context)
        {
            //cache = cache;
            this.context = context;//new ApplicationDbContextFactory().Create(new DbContextFactoryOptions()); 
        }

        public List<Building> GetAllBuildings()
        {
            return Enumerable.ToList<Building>(context.Buildings);
        }

        public async Task AddAsync(Building bld)
        {
            context.Buildings.Add(bld);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Building bld)
        {
            context.Buildings.Update(bld);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Building bld)
        {
            context.Remove(bld);
            await context.SaveChangesAsync();
        }

        public async Task<Building> GetBuildingByIdAsync(int buildingId)
        {
            return await context.Buildings.FindAsync(buildingId);
        }
    }
}