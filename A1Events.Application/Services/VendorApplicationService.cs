using A1Events.Application.DTOs;
using A1Events.Application.Interfaces;
using A1Events.Domain.Entities;

namespace A1Events.Application.Services;

public class VendorApplicationService : IVendorApplicationService
{
    private readonly IVendorApplicationRepository _repository;

    public VendorApplicationService(
        IVendorApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<VendorApplicationDto> CreateAsync(
        CreateVendorApplicationRequest request)
    {
        if (request.VendorId <= 0)
            throw new ArgumentException("Invalid vendor ID.");

        if (request.EventId <= 0)
            throw new ArgumentException("Invalid event ID.");

        var application = new VendorApplication
        {
            VendorId = request.VendorId,
            EventId = request.EventId
        };

        var createdApplication =
            await _repository.CreateAsync(application);

        return MapToDto(createdApplication);
    }

    public async Task<VendorApplicationDto?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid application ID.");

        var application =
            await _repository.GetByIdAsync(id);

        return application == null
            ? null
            : MapToDto(application);
    }

    public async Task<IEnumerable<VendorApplicationDto>> GetAllAsync()
    {
        var applications =
            await _repository.GetAllAsync();

        return applications.Select(MapToDto);
    }

    public async Task<IEnumerable<VendorApplicationDto>> GetByVendorAsync(
        int vendorId)
    {
        if (vendorId <= 0)
            throw new ArgumentException("Invalid vendor ID.");

        var applications =
            await _repository.GetByVendorAsync(vendorId);

        return applications.Select(MapToDto);
    }

    public async Task<IEnumerable<VendorApplicationDto>> GetByEventAsync(
        int eventId)
    {
        if (eventId <= 0)
            throw new ArgumentException("Invalid event ID.");

        var applications =
            await _repository.GetByEventAsync(eventId);

        return applications.Select(MapToDto);
    }

    public async Task<VendorApplicationDto?> ApproveAsync(
        int id,
        ReviewVendorApplicationRequest request)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid application ID.");

        var application =
            await _repository.GetByIdAsync(id);

        if (application == null)
            return null;

        if (application.Status !=
            A1Events.Domain.Enums.VendorApplicationStatus.Pending)
        {
            throw new ArgumentException(
                "Only pending applications can be approved.");
        }

        var approvedApplication =
            await _repository.ApproveAsync(
                id,
                request.AdminRemarks?.Trim());

        return approvedApplication == null
            ? null
            : MapToDto(approvedApplication);
    }

    public async Task<VendorApplicationDto?> RejectAsync(
        int id,
        ReviewVendorApplicationRequest request)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid application ID.");

        var application =
            await _repository.GetByIdAsync(id);

        if (application == null)
            return null;

        if (application.Status !=
            A1Events.Domain.Enums.VendorApplicationStatus.Pending)
        {
            throw new ArgumentException(
                "Only pending applications can be rejected.");
        }

        var rejectedApplication =
            await _repository.RejectAsync(
                id,
                request.AdminRemarks?.Trim());

        return rejectedApplication == null
            ? null
            : MapToDto(rejectedApplication);
    }

    private static VendorApplicationDto MapToDto(
        VendorApplication application)
    {
        return new VendorApplicationDto
        {
            Id = application.Id,
            VendorId = application.VendorId,
            EventId = application.EventId,
            Status = application.Status.ToString(),
            AdminRemarks = application.AdminRemarks,
            AppliedAt = application.AppliedAt
        };
    }
}