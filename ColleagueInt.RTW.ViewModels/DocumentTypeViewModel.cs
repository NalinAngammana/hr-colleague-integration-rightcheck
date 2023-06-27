using ColleagueInt.RTW.Database.Constants;

namespace ColleagueInt.RTW.ViewModels
{
    public class DocumentTypeViewModel
    {
        public int Id { get; set; }
        public string RTWDocumentName { get; set; }
        public string HCMDocumentName { get; set; }
        public long HCMDocumentTypeId { get; set; }
        public DocumentSection DocumentSection { get; set; }
        public string HCMVisaPermitTypeCode { get; set; }
        public string HCMVisaPermitTypeDescription { get; set; }
    }
}
