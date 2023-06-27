using AutoMapper;
using ColleagueInt.RTW.Consumer.Misc;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Repositories.Contracts;
using ColleagueInt.RTW.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ColleagueInt.RTW.Test.Services
{
    public class ColleagueServiceTests
    {
        private readonly ColleagueService _colleagueService;
        private readonly Mock<IColleagueRepository> _colleagueRepositoryMock = new Mock<IColleagueRepository>();
        private readonly Mock<IIncidentRepository> _incidentRepositoryMock = new Mock<IIncidentRepository>();

        private readonly IMapper _mapper;


        public ColleagueServiceTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = new Mapper(mockMapper);

            _colleagueService = new ColleagueService(_mapper, _colleagueRepositoryMock.Object, _incidentRepositoryMock.Object);
        }


        [Theory]
        [InlineData("123", "", "123_1")]
        [InlineData("123", null, "123_1")]
        [InlineData("123", "123_1", "123_2")]
        [InlineData("123", "123_99", "123_100")]
        [InlineData("123", "1234", "123_1234")]
        public async Task GetTrackingReferenceAsync_ShouldReturnNextTrackingReference(string colleagueNumber, string trackingReference, string expectedTrackingReference)
        {
            //Arrange
            var collegueMock = new Colleague
            {
                Id = 100,
                PersonNumber = colleagueNumber,
                TrackingReference = trackingReference
            };

            _colleagueRepositoryMock.Setup(x => x.GetLastColleagueEntryAsync(colleagueNumber)).ReturnsAsync(collegueMock);


            //Act
            var trackingReferenceResult = await _colleagueService.GetTrackingReferenceAsync(colleagueNumber);


            //Assert
            Assert.Equal(expectedTrackingReference, trackingReferenceResult);
        }


        [Fact]
        public async Task GetTrackingReferenceAsync_ShouldReturnNewTrackingReferenceForNewColleague()
        {
            //Arrange
            var colleagueNumber = "123";
            _colleagueRepositoryMock.Setup(x => x.GetLastColleagueEntryAsync(colleagueNumber)).ReturnsAsync(() => null);


            //Act
            var trackingReferenceResult = await _colleagueService.GetTrackingReferenceAsync(colleagueNumber);


            //Assert
            Assert.Equal("123_1", trackingReferenceResult);
        }


        [Fact]
        public async Task AbleToCreateCheckRequestForColleagueAsync_WhenExist_ShouldReturnTrue()
        {
            //Arrange
            var colleagueNumber = "100";
            _colleagueRepositoryMock.Setup(x => x.AbleToCreateCheckRequestForColleagueAsync(colleagueNumber)).ReturnsAsync(() => true);


            //Act
            var trackingReferenceResult = await _colleagueService.AbleToCreateCheckRequestForColleagueAsync(colleagueNumber);


            //Assert
            Assert.True(trackingReferenceResult);
        }


        [Fact]
        public async Task AbleToCreateCheckRequestForColleagueAsync_WhenNotExist_ShouldReturnFalse()
        {
            //Arrange
            var colleagueNumber = "100";
            _colleagueRepositoryMock.Setup(x => x.AbleToCreateCheckRequestForColleagueAsync(colleagueNumber)).ReturnsAsync(() => false);


            //Act
            var trackingReferenceResult = await _colleagueService.AbleToCreateCheckRequestForColleagueAsync(colleagueNumber);


            //Assert
            Assert.False(trackingReferenceResult);
        }



        [Fact]

        public async Task GetLastInsidentReportDateTimeAsync_WhenIncidentAvailable_RetunrDatetime()
        {
            //Arrange
            var incidentDate = new DateTime(2021, 11, 20);
            var latestIncident = new Incident()
            {
                Id = 2,
                IncidentDetailId = 4,
                Number = "123",
                Status = IncidentStatus.None,
                CreationTime = incidentDate,
                ServiceNowDescription = "B"
            };
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(IncidentErrorDescription.RTWAPIOutboundError);
            _incidentRepositoryMock.Setup(x => x.GetLastRecordedIncidentByErrorCodeAsync(errorCode)).ReturnsAsync(latestIncident);


            //Act
            var latIncidentDateResult = await _colleagueService.GetLastIncidentReportDateTimeAsync();


            //Assert
            Assert.Equal(incidentDate, latIncidentDateResult);
        }



        [Fact]

        public async Task GetLastInsidentReportDateTimeAsync_WhenIncidentNull_RetunrDatetime()
        {
            //Arrange
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(IncidentErrorDescription.RTWAPIOutboundError);
            _incidentRepositoryMock.Setup(x => x.GetLastRecordedIncidentByErrorCodeAsync(errorCode)).ReturnsAsync(() => null);


            //Act
            var latIncidentDateResult = await _colleagueService.GetLastIncidentReportDateTimeAsync();


            //Assert
            Assert.Equal(DateTime.MinValue, latIncidentDateResult);
        }


        [Fact]
        public async Task GetLatestCheckRequestFailedListAsync_ShouldColletion()
        {
            //Arrange
            var latestIncident = new Incident()
            {
                Id = 2,
                IncidentDetailId = 4,
                Number = "123",
                Status = IncidentStatus.None,
                CreationTime = new DateTime(2021, 11, 20),
                ServiceNowDescription = "B"
            };

            var failedChecks = new List<Colleague>()
            {
                new Colleague() {Id = 1, PersonNumber = "1", ErrorLog = "error1", TrackingReference = "1_1" },
                new Colleague() {Id = 2, PersonNumber = "2", ErrorLog = "error2", TrackingReference = "2_1" },
                new Colleague() {Id = 3, PersonNumber = "3", ErrorLog = "error3", TrackingReference = "3_1" },
            };
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(IncidentErrorDescription.RTWAPIOutboundError);

            _incidentRepositoryMock.Setup(x => x.GetLastRecordedIncidentByErrorCodeAsync(errorCode)).ReturnsAsync(latestIncident);

            _colleagueRepositoryMock.Setup(x => x.GetLatestCheckRequestFailedListAsync(latestIncident.CreationTime)).ReturnsAsync(failedChecks);


            //Act
            var failedListResult = await _colleagueService.GetLatestCheckRequestFailedListAsync();


            //Assert
            Assert.Equal(3, failedListResult.Count());
        }
    }
}
