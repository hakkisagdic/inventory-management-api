using AutoMapper;
using InventoryOrderManagement.Application.Common.CQS.Queries;
using InventoryOrderManagement.Application.Common.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryOrderManagement.Application.Features.CompanyManager.Queries;

public record GetCompanySingleDto
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Currency { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? ZipCode { get; init; }
    public string? Country { get; init; }
    public string? PhoneNumber { get; init; }
    public string? FaxNumber { get; init; }
    public string? EmailAddress { get; init; }
    public string? Website { get; init; }
    public DateTime? CreatedAtUtc { get; init; }
}

public class GetCompanySingleRequest : IRequest<GetCompanySingleResult>
{
    public string? Id { get; init; }
}

public class GetCompanySingleResult
{ 
    public GetCompanySingleDto? Data { get; init; }
}

public class GetCompanySingleHandler : IRequestHandler<GetCompanySingleRequest, GetCompanySingleResult>
{
    private readonly IMapper _mapper;
    private readonly IQueryContext _context;
    
    public GetCompanySingleHandler(IMapper mapper, IQueryContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<GetCompanySingleResult> Handle(GetCompanySingleRequest request, CancellationToken cancellationToken)
    {
        var query = _context.Company.AsNoTracking().ApplyIsDeletedFilter().AsQueryable();
        
        if (Guid.TryParse(request.Id, out var guidId))
        {
            var entity = await query.SingleOrDefaultAsync(x => x.Id == guidId, cancellationToken);
            
            if (entity == null)
                throw new Exception($"Company with id {request.Id} not found");
            
            var dto = _mapper.Map<GetCompanySingleDto>(entity);
            
            return new GetCompanySingleResult{ Data = dto };
        }
        
        throw new Exception($"Invalid company id format: {request.Id}");
    }
} 