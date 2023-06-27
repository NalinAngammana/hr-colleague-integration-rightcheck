using AutoMapper;
using ColleagueInt.RTW.Database.Entities;
using ColleagueInt.RTW.Database.Constants;
using ColleagueInt.RTW.ViewModels;
using Xunit;
using ColleagueInt.RTW.Consumer.Misc;

namespace ColleagueInt.RTW.Test.Consumer
{
    public class AutoMapperTests
    {
        private readonly IMapper _mapper;
        public AutoMapperTests()
        {
            // AutoMapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = new Mapper(mockMapper);
        }


        [Fact]
        public void AutoMapperLookupDataTests()
        {
            //Arrange
            var source = new FilterData
            {
                Id = 1,
                BusinessUnitName = "Argos Customer Management Centres",
                Description = "Include colleagues matching the filter criteria."                
            };

            //Act
            var result = _mapper.Map<FilterDataViewModel>(source);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("FilterDataViewModel", result.GetType().Name);
            Assert.Equal(source.Id, result.Id);
            Assert.Equal(source.BusinessUnitName, result.BusinessUnitName);
            Assert.Equal(source.Description, result.Description);
        }
    }
}
