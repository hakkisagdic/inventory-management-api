using InventoryOrderManagement.Application.Features.CompanyManager.Commands;
using InventoryOrderManagement.Application.Features.CompanyManager.Queries;
using InventoryOrderManagement.Presentation.Common.Base;
using InventoryOrderManagement.Presentation.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderManagement.Presentation.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CompanyController : BaseApiController
{
    public CompanyController(ISender sender) : base(sender)
    {
    }

    [HttpPost("AddCompany")]
    public async Task<ActionResult<ApiSuccessResult<AddCompanyResult>>> AddCompanyAsync(AddCompanyRequest request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);

        return Ok(new ApiSuccessResult<AddCompanyResult>
        {
            Code = StatusCodes.Status200OK,
            Message = $"Success executing {nameof(AddCompanyAsync)}",
            Content = response
        });
    }

    [HttpPost("UpdateCompany")]
    public async Task<ActionResult<ApiSuccessResult<UpdateCompanyResult>>> UpdateCompanyAsync(UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);

        return Ok(new ApiSuccessResult<UpdateCompanyResult>
        {
            Code = StatusCodes.Status200OK,
            Message = $"Success executing {nameof(UpdateCompanyAsync)}",
            Content = response
        });
    }

    [HttpGet("GetCompanySingle")]
    public async Task<ActionResult<ApiSuccessResult<GetCompanySingleResult>>> GetCompanySingleAsync(
        CancellationToken cancellationToken,
        [FromQuery] string id
        )
    {
        var request = new GetCompanySingleRequest { Id = id };
        var response = await _sender.Send(request, cancellationToken);

        return Ok(new ApiSuccessResult<GetCompanySingleResult>
        {
            Code = StatusCodes.Status200OK,
            Message = $"Success executing {nameof(GetCompanySingleAsync)}",
            Content = response
        });
    }

    [HttpGet("GetCompanyList")]
    public async Task<ActionResult<ApiSuccessResult<GetCompanyListResult>>> GetCompanyListAsync(
        CancellationToken cancellationToken,
        [FromQuery] bool isDeleted = false
        )
    {
        var request = new GetCompanyListRequest { IsDeleted = isDeleted };
        var response = await _sender.Send(request, cancellationToken);

        return Ok(new ApiSuccessResult<GetCompanyListResult>
        {
            Code = StatusCodes.Status200OK,
            Message = $"Success executing {nameof(GetCompanyListAsync)}",
            Content = response
        });
    }
}