namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    public interface IPropertyTypeService
    {
        Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync();

        Task AddAsync(PropertyType propertyType);

        Task UpdateAsync(PropertyType propertyType);

        Task DeleteAsync(PropertyType propertyType);

        Task<PropertyType> GetPropertyTypeByIdAsync(int propertyTypeId);
    }
}