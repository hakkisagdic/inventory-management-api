using FluentAssertions;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Application.Features.CompanyManager.Commands;
using InventoryOrderManagement.Core;
using Moq;

namespace InventoryOrderManagement.UnitTests.Features.CompanyManager.Commands;

public class AddCompanyHandlerTests
{
    private readonly Mock<ICommandRepository<Company>> _mockRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly AddCompanyHandler _handler;

    public AddCompanyHandlerTests()
    {
        _mockRepository = new Mock<ICommandRepository<Company>>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new AddCompanyHandler(_mockRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateCompany()
    {
        // Arrange
        var request = new AddCompanyRequest
        {
            Name = "Test Company",
            Currency = "USD",
            Street = "123 Main St",
            City = "Test City",
            State = "Test State",
            ZipCode = "12345",
            PhoneNumber = "123-456-7890",
            Email = "test@example.com"
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data!.Name.Should().Be(request.Name);
        result.Data.Currency.Should().Be(request.Currency);
        result.Data.Email.Should().Be(request.Email);
        result.Data.IsDeleted.Should().BeFalse();

        _mockRepository.Verify(x => x.CreateAsync(It.IsAny<Company>(), CancellationToken.None), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_WithCreatedById_ShouldSetCreatedById()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var request = new AddCompanyRequest
        {
            Name = "Test Company",
            Currency = "USD",
            Street = "123 Main St",
            City = "Test City",
            State = "Test State",
            ZipCode = "12345",
            PhoneNumber = "123-456-7890",
            Email = "test@example.com",
            CreatedById = userId
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data!.CreatedById.Should().Be(Guid.Parse(userId));

        _mockRepository.Verify(x => x.CreateAsync(It.IsAny<Company>(), CancellationToken.None), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveAsync(CancellationToken.None), Times.Once);
    }
} 