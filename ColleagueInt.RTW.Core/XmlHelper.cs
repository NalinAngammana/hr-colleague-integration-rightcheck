

namespace CloudColleaguePublisher.Core
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlHelper
    {
        public static T ToClass<T>(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(xmlString));
        }

        public static string FromClass<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }

        public static async Task<string> GetValueFromXml(string decodedXmlString, string requiredValue)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                Async = true
            };

            StringReader stringReader = new StringReader(decodedXmlString);
            using XmlReader reader = XmlReader.Create(stringReader, settings);
            while (await reader.ReadAsync())
            {
                if (reader.NodeType != XmlNodeType.Element)
                    continue;

                if (reader.Name == requiredValue)
                {
                    return await reader.ReadElementContentAsStringAsync();
                }
            }

            return string.Empty;
        }
    }
}
