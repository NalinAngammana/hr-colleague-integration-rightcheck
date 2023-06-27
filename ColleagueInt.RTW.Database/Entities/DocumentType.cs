using ColleagueInt.RTW.Database.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColleagueInt.RTW.Database.Entities
{
    [Table("DocumentTypes")]
    public class DocumentType : IdentityEntity
    {

        [Required]
        public string RTWDocumentName { get; set; }

        [Required]
        public string HCMDocumentName { get; set; }

        public long HCMDocumentTypeId { get; set; }

        public DocumentSection DocumentSection { get; set; }

        public string HCMVisaPermitTypeCode { get; set; }

        public string HCMVisaPermitTypeDescription { get; set; }
    }
}
