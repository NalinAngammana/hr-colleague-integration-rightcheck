using AutoMapper;
using ColleagueInt.RTW.ConsumerAPI.Data.Contracts;

namespace ColleagueInt.RTW.ConsumerAPI.Data
{
    public class DataMapper : IDataMapper
    {

        private readonly IMapper _mapper;

        public DataMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
       
    }
}
