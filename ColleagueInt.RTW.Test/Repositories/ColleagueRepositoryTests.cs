using ColleagueInt.RTW.Repositories.Contracts;
using System.Threading.Tasks;
using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Repositories;
using Xunit;
using ColleagueInt.RTW.Database.Entities;
using System.Linq;
using System;

namespace ColleagueInt.RTW.Test.Repositories
{
    public class ColleagueRepositoryTests
    {

        private readonly RTWContext _rtwContext;
        private IColleagueRepository _colleagueRepository;

        public ColleagueRepositoryTests()
        {
            _rtwContext = ContextCreator.CreateContextWithSeedData("ColleagueRepositoryTests");
            _colleagueRepository = new ColleagueRepository(_rtwContext);
        }

        [Theory]
        [InlineData("005", true)]   //New Entry
        [InlineData("006", true)]   //New Entry
        [InlineData("301", true)]   //Check Completed and User Removed
        [InlineData("302", true)]   //Check Completed and System Removed
        [InlineData("002", false)]  //HCM Updated
        [InlineData("003", false)]  //HCM Updated
        [InlineData("101", false)]  //Check Request Failed 
        [InlineData("004", false)]  //Inital Stage
        [InlineData("001", false)]  //Check Completed (2)
        [InlineData("303", false)]  //Check Completed (2)
        [InlineData("105", false)]  //Read Data Failed 
        [InlineData("106", false)]  //HCM Update Failed
        [InlineData("201", false)]  //Check Completed and Failed

        public async Task AbleToCreateCheckRequestForColleagueAsync_ReturnTrueOrFalse(string personNumber, bool expectedResult)
        {
            //Arrange

            //Act
            var result = await _colleagueRepository.AbleToCreateCheckRequestForColleagueAsync(personNumber);

            //Asert
            Assert.Equal(expectedResult, result);
        }


        [Theory]
        [InlineData("001", "001_2")]
        [InlineData("002", "002_3")]
        [InlineData("003", "003_1")]
        [InlineData("004", "004_2")]
        public async Task GetLastColleagueEntryAsync_ReturnLastRecordForColleague(string personNumber, string expectedResult)
        {
            //Arrange

            //Act
            var result = await _colleagueRepository.GetLastColleagueEntryAsync(personNumber);

            //Asert
            Assert.Equal(expectedResult, result.TrackingReference);
        }

        [Fact]
        public async Task GetLastColleagueEntryAsync_ReturnNullForNewColleague()
        {
            //Arrange

            //Act
            var result = await _colleagueRepository.GetLastColleagueEntryAsync("9009");

            //Asert
            Assert.Null(result);
        }


        [Theory]
        [InlineData("2021, 1, 01", 4)]
        [InlineData("2021, 1, 23", 1)]
        [InlineData("2021, 1, 21", 2)]
        [InlineData("2021, 1, 25", 0)]
        public async Task GetLatestCheckRequestFailedListAsync_ReturnCorrectFailedList(string lastIncidentRecordedDate, int expectedResultCount)
        {
            //Arrange

            //Act
            var result = await _colleagueRepository.GetLatestCheckRequestFailedListAsync(DateTime.Parse(lastIncidentRecordedDate));


            //Asert
            Assert.Equal(expectedResultCount, result.Count());
        }


        [Fact]
        public async Task GetLatestCheckRequestFailedListAsync_WhenNoLastIncident_ReturnAllCheckRequestFailed()
        {
            //Arrange
            var expectedResultCount = 5;

            //Act
            var result = await _colleagueRepository.GetLatestCheckRequestFailedListAsync(null);


            //Asert
            Assert.Equal(expectedResultCount, result.Count());
        }


        [Theory]
        [InlineData("005", true)]   //New Entry
        [InlineData("006", true)]   //New Entry
        [InlineData("301", true)]   //Check Completed and User Removed
        [InlineData("302", true)]   //Check Completed and System Removed
        [InlineData("003", true)]  //HCM Updated
        [InlineData("101", true)]  //Check Request Failed 
        [InlineData("303", true)]  //Check Completed (2)
        [InlineData("105", true)]  //Read Data Failed 
        [InlineData("106", true)]  //HCM Update Failed
        [InlineData("201", true)]  //Check Completed and Failed
        [InlineData("001", false)]  //Check Requested
        [InlineData("004", false)]  //Inital Stage


        public async Task AbleToCreateDocumentCheckRequestForColleagueAsync_ReturnTrueOrFalse(string personNumber, bool expectedResult)
        {
            //Arrange

            //Act
            var result = await _colleagueRepository.AbleToCreateDocumentCheckRequestForColleagueAsync(personNumber);

            //Asert
            Assert.Equal(expectedResult, result);
        }

    }
}
