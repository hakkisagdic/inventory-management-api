using AutoMapper;
using FluentAssertions;
using InventoryOrderManagement.Application.Common.CQS.Queries;
using InventoryOrderManagement.Application.Features.CompanyManager.Queries;
using InventoryOrderManagement.Core;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace InventoryOrderManagement.UnitTests.Features.CompanyManager.Queries;

public class GetCompanyListHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IQueryContext> _mockContext;
    private readonly GetCompanyListHandler _handler;

    public GetCompanyListHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockContext = new Mock<IQueryContext>();
        _handler = new GetCompanyListHandler(_mockMapper.Object, _mockContext.Object);
    }

    [Fact(Skip = "Needs refactoring of the handling code to make it testable")]
    public async Task Handle_ValidRequest_ShouldReturnCompanyList()
    {
        // Arrange
        var companies = new List<Company>
        {
            new Company { Id = Guid.NewGuid(), Name = "Company 1", Email = "email1@example.com", IsDeleted = false },
            new Company { Id = Guid.NewGuid(), Name = "Company 2", Email = "email2@example.com", IsDeleted = false }
        };
        
        // Set up mock context to return the companies list when we use Set<Company>
        _mockContext.Setup(c => c.Set<Company>()).Returns(companies.AsQueryable());
        
        // Set up the mapper
        var mappedCompanies = new List<GetCompanyListDto>
        {
            new GetCompanyListDto { Id = companies[0].Id.ToString(), Name = "Company 1", EmailAddress = "email1@example.com" },
            new GetCompanyListDto { Id = companies[1].Id.ToString(), Name = "Company 2", EmailAddress = "email2@example.com" }
        };
        
        _mockMapper.Setup(m => m.Map<List<GetCompanyListDto>>(It.IsAny<List<Company>>()))
            .Returns(mappedCompanies);

        var request = new GetCompanyListRequest { IsDeleted = false };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(2);
        result.Data![0].Name.Should().Be("Company 1");
        result.Data![1].Name.Should().Be("Company 2");
    }
} 