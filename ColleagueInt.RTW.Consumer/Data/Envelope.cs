namespace ColleagueInt.RTW.Consumer.Data
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2003/05/soap-envelope", IsNullable = false)]
    public partial class Envelope
    {
        public object Header { get; set; }
        public EnvelopeBody Body { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public partial class EnvelopeBody
    {
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://xmlns.oracle.com/oxp/service/PublicReportService")]
        public runReportResponse runReportResponse { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmlns.oracle.com/oxp/service/PublicReportService")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://xmlns.oracle.com/oxp/service/PublicReportService", IsNullable = false)]
    public partial class runReportResponse
    {
        public runReportResponseRunReportReturn runReportReturn { get; set; }       
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmlns.oracle.com/oxp/service/PublicReportService")]
    public partial class runReportResponseRunReportReturn
    {
        public byte[] reportBytes { get; set; }
        
        public string reportContentType { get; set; }
       
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        
        public object reportFileID { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public object reportLocale { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public object metaDataList { get; set; }
    }
}
