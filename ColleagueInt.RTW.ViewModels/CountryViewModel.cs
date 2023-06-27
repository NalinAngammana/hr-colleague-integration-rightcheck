
using ColleagueInt.RTW.Database.Constants;

namespace ColleagueInt.RTW.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string RTWCountryCode { get; set; }
        public string RTWCountryName { get; set; }
        public string HCMCountryCode { get; set; }
        public string HCMCountryName { get; set; }
    }
}
