using A1Events.Domain.Entities;

namespace A1Events.Application.Interfaces;

public interface IEventRepository
{
    Task<List<Event>> GetAllAsync();

    Task<Event?> GetByIdAsync(int id);

    Task<List<Event>> GetUpcomingAsync();

    Task<Event> AddAsync(Event eventEntity);

    Task<Event?> UpdateAsync(Event eventEntity);

    Task<bool> DeleteAsync(int id);

    Task<Event?> PublishAsync(int id);

    Task<Event?> CancelAsync(int id);
}