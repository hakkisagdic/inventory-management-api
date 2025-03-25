using FluentAssertions;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Application.Features.CompanyManager.Commands;
using InventoryOrderManagement.Core;
using Moq;

namespace InventoryOrderManagement.UnitTests.Features.CompanyManager.Commands;

public class UpdateCompanyHandlerTests
{
    private readonly Mock<ICommandRepository<Company>> _mockRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly UpdateCompanyHandler _updateCompanyHandler;
    
    public UpdateCompanyHandlerTests()
    {
        _mockRepository = new Mock<ICommandRepository<Company>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _updateCompanyHandler = new UpdateCompanyHandler(_mockRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldUpdateCompany()
    {
        //Arrange
        var company = new Company { Id = Guid.NewGuid(), Name = "Test Company", Email = "email1@example.com", IsDeleted = false };
        
        _mockRepository.Setup(r => r.GetAsync(company.Id.ToString(), It.IsAny<CancellationToken>())).ReturnsAsync(company);
        
        var request = new UpdateCompanyRequest()
        {
            Id = company.Id.ToString(),
            Name = "Updated Company",
            Email = company.Email
        };
        
        //Act
        var result = await _updateCompanyHandler.Handle(request, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(company.Id);
        result.Data.IsDeleted.Should().BeFalse();

        _mockRepository.Verify(x => x.Update(It.IsAny<Company>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveAsync(CancellationToken.None), Times.Once);
    }
}