namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Persistence.Models;

    public interface IMeetingService
    {
        Task<List<Meeting>> GetAllMeetings();

        Task AddAsync(Meeting meeting);

        Task UpdateAsync(Meeting meeting);

        Task DeleteAsync(int id);

        Task<Meeting> GetMeetingByIdAsync(int id);
    }
}