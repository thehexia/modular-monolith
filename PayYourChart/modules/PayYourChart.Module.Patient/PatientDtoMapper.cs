using AutoMapper;

namespace PayYourChart.Module.Patient;

internal interface IPatientDtoMapperFactory
{
    IMapper Get();
}


internal class PatientDtoMapperFactory : IPatientDtoMapperFactory
{
    readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientDto>()));

    public IMapper Get() => _mapper;
}
