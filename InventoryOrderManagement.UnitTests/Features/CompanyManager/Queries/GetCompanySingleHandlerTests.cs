using AutoMapper;
using FluentAssertions;
using InventoryOrderManagement.Application.Common.CQS.Queries;
using InventoryOrderManagement.Application.Features.CompanyManager.Queries;
using InventoryOrderManagement.Core;
using Moq;

namespace InventoryOrderManagement.UnitTests.Features.CompanyManager.Queries;

public class GetCompanySingleHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IQueryContext> _mockContext;
    private readonly GetCompanySingleHandler _handler;
    
    public GetCompanySingleHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockContext = new Mock<IQueryContext>();
        _handler = new GetCompanySingleHandler(_mockMapper.Object, _mockContext.Object);
    }

    [Fact(Skip = "Needs refactoring of the handling code to make it testable")]
    public async Task Handle_ValidRequest_ShouldReturnCompanySingle()
    {
        //Arrange
        var company = new Company { Id = Guid.NewGuid(), Name = "Company 1", Email = "email1@example.com", IsDeleted = false };
        
        _mockContext.Setup(c => c.Company.First()).Returns(company);

        // Set up the mapper
        var mappedCompany = new GetCompanySingleDto()
            { Id = company.Id.ToString(), Name = "Company 1", EmailAddress = "email1@example.com" };
        
        _mockMapper.Setup(m => m.Map<GetCompanySingleDto>(It.IsAny<Company>()))
            .Returns(mappedCompany);
        
        var request = new GetCompanySingleRequest() { Id = company.Id.ToString() };
        
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Should().Be(mappedCompany);
        result.Data.Name.Should().Be("Company 1");
    }

}