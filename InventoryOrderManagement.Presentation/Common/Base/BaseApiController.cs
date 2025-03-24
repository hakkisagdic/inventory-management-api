using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace InventoryOrderManagement.Presentation.Common.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly ISender _sender;
    
    protected BaseApiController(ISender sender)
    {
        _sender = sender;
    }
}