using AutoMapper;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.ViewModels;

namespace ColleagueInt.RTW.ConsumerAPI.Misc
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
        }
    }
}
