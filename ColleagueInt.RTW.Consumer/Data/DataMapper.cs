using AutoMapper;
using ColleagueInt.RTW.Consumer.Data.Contracts;
using System;
using System.Collections.Generic;
using static ColleagueInt.RTW.Consumer.Data.RTWData;

namespace ColleagueInt.RTW.Consumer.Data
{
    public class DataMapper : IDataMapper
    {

        private readonly IMapper _mapper;

        public DataMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PersonCheckRequest GenerateRTWColleagueObject(ColleagueEntry colleagueEntry, string trackingReference)
        {
            var colleagueDetails = _mapper.Map<PersonCheckRequest>(colleagueEntry);
                colleagueDetails.TrackingReference = trackingReference;

            return colleagueDetails;
        }

        public PersonCheckRequest GenerateRTWColleagueObject(ColleagueEntry colleagueEntry, string trackingReference, string lineManagerCostCentre)
        {
            var colleagueDetails = _mapper.Map<PersonCheckRequest>(colleagueEntry);
            colleagueDetails.TrackingReference = trackingReference;
            colleagueDetails.LocationId = lineManagerCostCentre;

            return colleagueDetails;
        }

  

        public PersonStatus GenerateRTWColleagueStatusObject(bool status)
        {
            var personStatus = new PersonStatus
            {
                ReviewStatus = status?"My HR" : "My HR update failed"
            };

            return personStatus;
        }
    }
}
