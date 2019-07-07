namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Persistence.Models;

    public class PorpertyTypeService : IPropertyTypeService
    {
        private readonly AppDbContext context;

        public PorpertyTypeService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync()
        {
            return await context.PropertyTypes.ToListAsync();
        }

        public async Task AddAsync(PropertyType propertyType)
        {
            context.PropertyTypes.Add(propertyType);

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PropertyType propertyType)
        {
            context.PropertyTypes.Update(propertyType);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PropertyType propertyType)
        {
            context.PropertyTypes.Remove(propertyType);
            await context.SaveChangesAsync();
        }

        public async Task<PropertyType> GetPropertyTypeByIdAsync(int propertyTypeId)
        {
            return await context.PropertyTypes.FindAsync(propertyTypeId);
        }
    }
}