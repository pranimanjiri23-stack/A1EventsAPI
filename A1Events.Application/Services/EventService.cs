using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;

namespace A1Events.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await _eventRepository.GetAllAsync();

        return events.Select(MapToDto);
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);

        return eventEntity == null
            ? null
            : MapToDto(eventEntity);
    }

    public async Task<IEnumerable<EventDto>> GetUpcomingAsync()
    {
        var events = await _eventRepository.GetUpcomingAsync();

        return events.Select(MapToDto);
    }

    private static EventDto MapToDto(A1Events.Domain.Entities.Event eventEntity)
    {
        return new EventDto
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            EventDate = eventEntity.EventDate,
            StartTime = eventEntity.StartTime,
            EndTime = eventEntity.EndTime,
            Location = eventEntity.Location,
            ImageUrl = eventEntity.ImageUrl,
            Status = eventEntity.Status.ToString()
        };
    }


    public async Task<EventDto> CreateAsync(CreateEventRequest request)
    {
        if (request.EventDate.Date < DateTime.Today)
        {
            throw new ArgumentException(
                "Event date cannot be in the past.");
        }

        if (request.StartTime >= request.EndTime)
        {
            throw new ArgumentException(
                "Start time must be earlier than end time.");
        }

        var eventEntity = new A1Events.Domain.Entities.Event
        {
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            EventDate = request.EventDate.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location.Trim(),
            ImageUrl = request.ImageUrl?.Trim(),

            Status = A1Events.Domain.Enums.EventStatus.Draft,

            CreatedAt = DateTime.UtcNow
        };

        var createdEvent = await _eventRepository.AddAsync(eventEntity);

        return MapToDto(createdEvent);
    }

    public async Task<EventDto?> UpdateAsync(int id, UpdateEventRequest request)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid event ID.");

        if (request.EventDate.Date < DateTime.Today)
            throw new ArgumentException("Event date cannot be in the past.");

        if (request.StartTime >= request.EndTime)
            throw new ArgumentException("Start time must be earlier than end time.");

        var existingEvent = await _eventRepository.GetByIdAsync(id);

        if (existingEvent == null)
            return null;

        existingEvent.Name = request.Name.Trim();
        existingEvent.Description = request.Description.Trim();
        existingEvent.EventDate = request.EventDate.Date;
        existingEvent.StartTime = request.StartTime;
        existingEvent.EndTime = request.EndTime;
        existingEvent.Location = request.Location.Trim();
        existingEvent.ImageUrl = request.ImageUrl?.Trim();

        var updatedEvent = await _eventRepository.UpdateAsync(existingEvent);

        return updatedEvent == null ? null : MapToDto(updatedEvent);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid event ID.");

        return await _eventRepository.DeleteAsync(id);
    }

    public async Task<EventDto?> PublishAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid event ID.");

        var eventEntity = await _eventRepository.PublishAsync(id);

        return eventEntity == null ? null : MapToDto(eventEntity);
    }

    public async Task<EventDto?> CancelAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid event ID.");

        var eventEntity = await _eventRepository.CancelAsync(id);

        return eventEntity == null ? null : MapToDto(eventEntity);
    }

}