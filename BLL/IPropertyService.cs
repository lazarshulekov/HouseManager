using System.Collections.Generic;

namespace BLL
{
    using System.Threading.Tasks;

    using DAL.Models;

    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();

        Task<IEnumerable<Property>> GetAllPropertiesByBuildingIdAsync(int id);

        Task AddAsync(Property prop, int buildingId);

        Task UpdateAsync(Property prop);

        Task DeleteAsync(int propertyId);

        Task<Property> GetPropertyByIdAsync(int propertyId);
    }
}