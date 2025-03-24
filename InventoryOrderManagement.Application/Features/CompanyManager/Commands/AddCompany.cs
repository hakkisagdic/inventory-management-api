using FluentValidation;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core;
using MediatR;

namespace InventoryOrderManagement.Application.Features.CompanyManager.Commands;

public class AddCompanyResult
{
    public Company? Data { get; set; }
}

public class AddCompanyRequest : IRequest<AddCompanyResult>
{
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
    public string? Email { get; init; }
    public string? Website { get; init; }
    public string? CreatedById { get; init; }
}

public class AddCompanyValidator : AbstractValidator<AddCompanyRequest>
{
    public AddCompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Currency).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
        RuleFor(x => x.ZipCode).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
    }
}

public class AddCompanyHandler : IRequestHandler<AddCompanyRequest, AddCompanyResult>
{
    private readonly ICommandRepository<Company> _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddCompanyHandler(ICommandRepository<Company> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AddCompanyResult> Handle(AddCompanyRequest request, CancellationToken cancellationToken)
    {
        var entity = new Company
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Currency = request.Currency,
            Street = request.Street,
            City = request.City,
            State = request.State,
            ZipCode = request.ZipCode,
            Country = request.Country,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Website = request.Website,
            IsDeleted = false
        };
        
        if (!string.IsNullOrEmpty(request.CreatedById) && Guid.TryParse(request.CreatedById, out var createdByGuid))
        {
            entity.CreatedById = createdByGuid;
        }
        
        await _repository.CreateAsync(entity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return new AddCompanyResult{ Data = entity };
    }
} 