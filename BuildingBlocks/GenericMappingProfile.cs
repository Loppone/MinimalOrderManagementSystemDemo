using AutoMapper;

namespace GenericService
{
    public class GenericMappingProfile<TMODEL, TDATA> : Profile
    {
        public GenericMappingProfile()
        {
            CreateMap<TMODEL, TDATA>();
            CreateMap<TDATA, TMODEL>();
        }
    }
}
