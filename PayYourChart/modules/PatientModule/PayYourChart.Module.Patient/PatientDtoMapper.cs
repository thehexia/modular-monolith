using AutoMapper;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Patient;

internal interface IPatientDtoMapper
{
    IMapper Get();
}


internal class PatientDtoMapper : IPatientDtoMapper
{
    readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => 
        { 
            cfg.CreateMap<Patient, PatientDto>(); 
            cfg.CreateMap<GetItemResponse, LineItem>()
                .ForMember(dst => dst.ItemCatalogId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dst => dst.Description, opts => opts.MapFrom(src => 
                    !src.SpecialApprovalRequired ? src.Description : string.Format("{0} - Special - Reason: {1}", src.Description, src.SpecialApprovalReason)));
        }));
    public IMapper Get() => _mapper;
}
