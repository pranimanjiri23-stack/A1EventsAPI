using A1Events.Application.DTOs;

namespace A1Events.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllAsync();

    Task<EventDto?> GetByIdAsync(int id);

    Task<IEnumerable<EventDto>> GetUpcomingAsync();

    Task<EventDto> CreateAsync(CreateEventRequest request);

    Task<EventDto?> UpdateAsync(int id,UpdateEventRequest request);

    Task<bool> DeleteAsync(int id);

    Task<EventDto?> PublishAsync(int id);

    Task<EventDto?> CancelAsync(int id);
}