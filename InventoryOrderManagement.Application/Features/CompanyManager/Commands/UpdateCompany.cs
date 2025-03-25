using FluentValidation;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core;
using MediatR;

namespace InventoryOrderManagement.Application.Features.CompanyManager.Commands;

public class UpdateCompanyResult
{
    public Company? Data { get; set; }
}

public class UpdateCompanyRequest : IRequest<UpdateCompanyResult>
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
    public string? Email { get; init; }
    public string? Website { get; init; }
    public string? UpdatedById { get; init; }
}

public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
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

public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyRequest, UpdateCompanyResult>
{
    private readonly ICommandRepository<Company> _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateCompanyHandler(ICommandRepository<Company> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UpdateCompanyResult> Handle(UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Id ?? string.Empty, cancellationToken);
        
        if (entity == null)
            throw new Exception($"Company with id {request.Id} not found");
        
        if (!string.IsNullOrEmpty(request.UpdatedById) && Guid.TryParse(request.UpdatedById, out var updatedByGuid))
        {
            entity.UpdatedById = updatedByGuid;
        }
        else if (entity.UpdatedById == Guid.Empty)
        {
            entity.UpdatedById = Guid.NewGuid();
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Currency = request.Currency;
        entity.Street = request.Street;
        entity.City = request.City;
        entity.State = request.State;
        entity.ZipCode = request.ZipCode;
        entity.Country = request.Country;
        entity.PhoneNumber = request.PhoneNumber;
        entity.Email = request.Email;
        entity.Website = request.Website;
        
        _repository.Update(entity);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return new UpdateCompanyResult{ Data = entity };
    }
}