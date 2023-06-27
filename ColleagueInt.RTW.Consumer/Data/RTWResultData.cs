using System;
using System.Collections.Generic;
using System.Text;

namespace ColleagueInt.RTW.Consumer.Data
{

    public class Person
    {
        public Persondetails personDetails { get; set; }
        public string organizationId { get; set; }
    }

    public class Persondetails
    {
        public string personId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string sex { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string nationality { get; set; }
        public string reviewStatus { get; set; }
        public string customField1 { get; set; }
        public string customField2 { get; set; }
        public string currentStatus { get; set; }
        public object validUntil { get; set; }
        public object leftOrganizationOn { get; set; }
        public DateTime createdDate { get; set; }
        public bool hasProfileImage { get; set; }
        public string failureReason { get; set; }
        public object failureReasonAdditionalDetails { get; set; }
        public int totalDocumentSets { get; set; }
        public string documentSets { get; set; }
    }

    public class CurrentDocumentSet
    {
        public Document[] documents { get; set; }
        public string documentSetDetailsUri { get; set; }
        public string documentSetId { get; set; }
        public DateTime createdDate { get; set; }
        public string personId { get; set; }
        public string personStatus { get; set; }
        public int totalDocumentsInSet { get; set; }
    }

    public class Document
    {
        public string documentId { get; set; }
        public Documenttype documentType { get; set; }
        public DateTime createdDate { get; set; }
        public object VisaType { get; set; }
        public object VisaTypeV2 { get; set; }
        public Documentkeyvaluepair[] documentKeyValuePairs { get; set; }
        public string documentImages { get; set; }
        public string organizationId { get; set; }
    }

    public class Documenttype
    {
        public string name { get; set; }
        public Pagedocumenttype[] pageDocumentTypes { get; set; }
    }

    public class Pagedocumenttype
    {
        public Page page { get; set; }
    }

    public class Page
    {
        public string name { get; set; }
        public string systemName { get; set; }
        public int pageNumber { get; set; }
        public Physicaldocument physicalDocument { get; set; }
    }

    public class Physicaldocument
    {
        public string name { get; set; }
    }

    public class Documentkeyvaluepair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }


    public class DocumentImage
    {
        public Image[] image { get; set; }
    }

    public class Image
    {
        public string mimeType { get; set; }
        public string imageData { get; set; }
    }



}
