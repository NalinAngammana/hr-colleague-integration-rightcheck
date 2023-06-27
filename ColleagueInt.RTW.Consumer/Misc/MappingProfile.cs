using AutoMapper;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.ViewModels;
using static ColleagueInt.RTW.Consumer.Data.RTWData;

namespace ColleagueInt.RTW.Consumer.Misc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppSetttingsViewModel, AppSettings>();
            CreateMap<AppSettings, AppSetttingsViewModel>();

            CreateMap<IncidentViewModel, IncidentDetail>();
            CreateMap<IncidentDetail, IncidentViewModel>();

            CreateMap<IncidentViewModel, Incident>();
            CreateMap<Incident, IncidentViewModel>();

            CreateMap<FilterData, FilterDataViewModel>();

            CreateMap<Colleague, ColleagueViewModel>();
            CreateMap<ColleagueViewModel, Colleague>();

            CreateMap<ColleagueEntry, PersonCheckRequest> ()
                 .ForMember(dest => dest.FirstName, opts  => opts.MapFrom(src => src.FirstName))
                 .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.LocationId, opts => opts.MapFrom(src => src.Costcenter))
                 .ForMember(dest => dest.AdditionalIdentifier, opts => opts.MapFrom(src => src.PersonNumber));

            CreateMap<DocumentType, DocumentTypeViewModel>();

            CreateMap<Country, CountryViewModel>();

            CreateMap<InboundErrorLog, InboundErrorLogViewModel>();
            CreateMap<InboundErrorLogViewModel, InboundErrorLog>();
        }
    }
}
