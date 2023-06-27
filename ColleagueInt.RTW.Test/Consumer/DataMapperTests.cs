using AutoMapper;
using ColleagueInt.RTW.Consumer.Data;
using ColleagueInt.RTW.Consumer.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ColleagueInt.RTW.Test.Consumer
{
    public class DataMapperTests
    {
        private readonly IMapper _mapper;
        private readonly DataMapper _dataMapper;


        public DataMapperTests()
        {
            // AutoMapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = new Mapper(mockMapper);

            _dataMapper = new DataMapper(_mapper);
        }

    

        [Fact]
        public void GetRTWColleageDetailsTest()
        {
            //Arrange
            var trackingReference = "1234_12";
            var colleagueEntry = new ColleagueEntry()
            {
                FirstName = "First Name",
                LastName  = "Lat Name",
                Costcenter  = "Location Name",
                PersonNumber = "1234"
            };


            //Act
            var result = _dataMapper.GenerateRTWColleagueObject(colleagueEntry, trackingReference);


            //Assert
            Assert.Equal(colleagueEntry.FirstName, result.FirstName);
            Assert.Equal(colleagueEntry.LastName, result.LastName);
            Assert.Equal(colleagueEntry.Costcenter, result.LocationId);
            Assert.Equal(colleagueEntry.PersonNumber, result.AdditionalIdentifier);
            Assert.Equal(trackingReference, result.TrackingReference);

        }
    }
}
