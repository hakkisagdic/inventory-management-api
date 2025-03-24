using AutoMapper;
using InventoryOrderManagement.Application.Features.CompanyManager.Queries;
using InventoryOrderManagement.Core;

namespace InventoryOrderManagement.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, GetCompanyListDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email));
            
        CreateMap<Company, GetCompanySingleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email));
    }
} 