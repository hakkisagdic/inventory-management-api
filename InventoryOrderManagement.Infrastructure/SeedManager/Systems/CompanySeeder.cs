using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core;
using System.Linq;

namespace InventoryOrderManagement.Infrastructure.SeedManager.Systems;

public class CompanySeeder
{
    private readonly ICommandRepository<Company> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQueryRepository<Company> _queryRepository;
    
    public CompanySeeder(
        ICommandRepository<Company> repository, 
        IUnitOfWork unitOfWork,
        IQueryRepository<Company> queryRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _queryRepository = queryRepository;
    }
    
    public async Task GenerateDataAsync()
    {
        // Şirket kaydı var mı kontrol et
        var existingCompany = await _queryRepository.FirstOrDefaultAsync(c => c.Name == "Acme Corp");
        
        if (existingCompany != null)
        {
            // Zaten var, yeni eklemiyoruz
            return;
        }
        
        var entity = new Company
        {
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
            Name = "Acme Corp",
            Currency = "USD",
            Street = "123 Main St",
            City = "Metropolis",
            State = "New York",
            ZipCode = "10001",
            Country = "USA",
            PhoneNumber = "+1-212-555-1234",
            Email = "info@acmecorp.com",
            Website = "https://www.acmecorp.com"
        };

        await _repository.CreateAsync(entity);
        await _unitOfWork.SaveAsync();
    }
}