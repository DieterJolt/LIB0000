using System.IO;
using System.Xml.Serialization;

namespace LIB0000.Services
{
    public partial class XmlService : ObservableObject
    {


        #region Commands
        #endregion

        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public T DeserializeXmlToObject<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(xml))
            {
                return (T)xs.Deserialize(textReader);
            }
        }

        public string SerializeObjectToXml<T>(T obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                xs.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        #endregion

        #region Properties
        [ObservableProperty]
        public string _databasePath;

        #endregion


    }
}
