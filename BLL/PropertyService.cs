namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Models;

    using Microsoft.EntityFrameworkCore;

    using Persistence.Models;

    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext context;

        private readonly IMapper mapper;

        public PropertyService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await context.Properties.Include(p => p.PropertyType).Include(p => p.AppUser).Include(p => p.BuildingProperties).ThenInclude(x => x.Building).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesByBuildingIdAsync(int id)
        {
            var properties = from c in context.Properties.Include(x => x.AppUser).Include(x => x.PropertyType)
                           join t in context.BuildingProperties on c.Id equals t.PropertyId
                           where t.BuildingId == id
                           select c;
            return properties;
        }

        public async Task AddAsync(Property property, int buildingId)
        {
            var entity = context.Properties.Add(property);

            await context.SaveChangesAsync();

            context.BuildingProperties.Add(
                new BuildingProperties() { BuildingId = buildingId, PropertyId = entity.Entity.Id });

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            context.Properties.Update(property);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int propertyId)
        {
            var entity = await context.Properties.FindAsync(propertyId);
            
            context.Properties.Remove(entity);

            await context.SaveChangesAsync();
        }

        public async Task<Property> GetPropertyByIdAsync(int propertyId)
        {
            return await context.Properties.FindAsync(propertyId);
        }
    }
}