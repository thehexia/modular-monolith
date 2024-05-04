using AutoMapper;

namespace PayYourChart.Module.Patient;

internal interface IPatientDtoMapper
{
    IMapper Get();
}


internal class PatientDtoMapper : IPatientDtoMapper
{
    readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientDto>()));
    public IMapper Get() => _mapper;
}
