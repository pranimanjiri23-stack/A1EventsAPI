using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;
using A1Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace A1Events.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly A1EventsDbContext _context;

    public EventRepository(A1EventsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _context.Events
            .FromSqlRaw("EXEC dbo.usp_Event_GetAll")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        var result = await _context.Events
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Event_GetById @Id={id}")
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<List<Event>> GetUpcomingAsync()
    {
        return await _context.Events
            .FromSqlRaw("EXEC dbo.usp_Event_GetUpcoming")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Event> AddAsync(Event eventEntity)
    {
        var result = await _context.Events
            .FromSqlInterpolated($"""
            EXEC dbo.usp_Event_Create
                @Name={eventEntity.Name},
                @Description={eventEntity.Description},
                @EventDate={eventEntity.EventDate},
                @StartTime={eventEntity.StartTime},
                @EndTime={eventEntity.EndTime},
                @Location={eventEntity.Location},
                @ImageUrl={eventEntity.ImageUrl}
            """)
            .AsNoTracking()
            .ToListAsync();

        return result.Single();
    }

    public async Task<Event?> UpdateAsync(Event eventEntity)
    {
        var result = await _context.Events
            .FromSqlInterpolated($"""
            EXEC dbo.usp_Event_Update
                @Id={eventEntity.Id},
                @Name={eventEntity.Name},
                @Description={eventEntity.Description},
                @EventDate={eventEntity.EventDate},
                @StartTime={eventEntity.StartTime},
                @EndTime={eventEntity.EndTime},
                @Location={eventEntity.Location},
                @ImageUrl={eventEntity.ImageUrl}
            """)
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.Database
            .SqlQuery<int>(
                $"EXEC dbo.usp_Event_Delete @Id={id}")
            .ToListAsync();

        return result.FirstOrDefault() > 0;
    }

    public async Task<Event?> PublishAsync(int id)
    {
        var result = await _context.Events
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Event_Publish @Id={id}")
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<Event?> CancelAsync(int id)
    {
        var result = await _context.Events
            .FromSqlInterpolated(
                $"EXEC dbo.usp_Event_Cancel @Id={id}")
            .AsNoTracking()
            .ToListAsync();

        return result.FirstOrDefault();
    }
}