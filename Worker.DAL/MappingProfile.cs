using AutoMapper;

namespace Worker.DAL
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.WorkerProfile, Entities.WorkerProfile>();
            CreateMap<Models.Address, Entities.Address>();
            CreateMap<Models.Skill, Entities.Skill>();
            CreateMap<Entities.WorkerProfile, Models.WorkerProfile>();
            CreateMap<Entities.Address, Models.Address>();
            CreateMap<Entities.Skill, Models.Skill>();
        }
    }
}