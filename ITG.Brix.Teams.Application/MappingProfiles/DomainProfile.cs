using AutoMapper;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Domain;
using System;

namespace ITG.Brix.Teams.Application.MappingProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            //CreateMap<Source, string>().ConvertUsing(x => x.Name);

            //CreateMap<Site, string>().ConvertUsing(x => x.Name);

            //CreateMap<Operation, string>().ConvertUsing(x => x.Name);

            //CreateMap<OperationalDepartment, string>().ConvertUsing(x => x.Name);

            //CreateMap<Zone, string>().ConvertUsing(x => x.Name);

            //CreateMap<TypePlanning, string>().ConvertUsing(x => x.Name);

            //CreateMap<Customer, string>().ConvertUsing(x => x.Name);

            //CreateMap<ProductionSite, string>().ConvertUsing(x => x.Name);

            //CreateMap<TransportType, string>().ConvertUsing(x => x.Name);

            CreateMap<DriverWait, string>().ConvertUsing(x => x.Name);

            CreateMap<Team, TeamModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (string)src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => (string)src.Description))
                .ForMember(dest => dest.Layout, opt => opt.MapFrom(src => (Guid)src.Layout))
                                        .ForMember(x => x.Members, opt => opt.Ignore());

            CreateMap<Operator, MemberModel>();
        }
    }
}
