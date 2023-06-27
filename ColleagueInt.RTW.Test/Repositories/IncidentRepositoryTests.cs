using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Repositories;
using ColleagueInt.RTW.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ColleagueInt.RTW.Test.Repositories
{
    public class IncidentRepositoryTests
    {

        private readonly RTWContext _rtwContext;
        private IIncidentRepository _incidentRepository;

        public IncidentRepositoryTests()
        {
            _rtwContext = ContextCreator.CreateContextWithSeedData("IncidentRepositoryTests");
            _incidentRepository = new IncidentRepository(_rtwContext);
        }

        [Fact]
        public async Task GetLastRecordedIncidentByErrorCodeAsync_WhenDataAvailable_ReturnLastIncident()
        {
            //Arrange
            var expectedIncident = new Incident()
            {
                Id = 2,
                IncidentDetailId = 4,
                ServiceNowDescription = "B"
            };
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(IncidentErrorDescription.RTWAPIOutboundError);

            //Act
            var result = await _incidentRepository.GetLastRecordedIncidentByErrorCodeAsync(errorCode);

            //Asert
            Assert.Equal(expectedIncident.Id, result.Id);
            Assert.Equal(expectedIncident.ServiceNowDescription, result.ServiceNowDescription);
        }

        [Fact]
        public async Task GetLastRecordedIncidentByErrorCodeAsync_WhenNoData_ReturnNull()
        {
            //Arrange
            var errorCode = IncidentErrorCodes.Instance.GetIncidentErrorCode(IncidentErrorDescription.NoIncident);

            //Act
            var result = await _incidentRepository.GetLastRecordedIncidentByErrorCodeAsync(errorCode);

            //Asert
            Assert.Null(result);
        }
    }
}
