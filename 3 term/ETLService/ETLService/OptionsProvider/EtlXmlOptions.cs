using ETLService.Option;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Utilities;

namespace ETLService.OptionProvider
{
    class EtlXmlOptions : OptionsProvider
    {
        public EtlXmlOptions(Type settingsType,string xmlProviderPath) : base(settingsType,xmlProviderPath)
        {

        }
        public override Option<T> GetOption<T>()
        {
            Option<T> result;

            string innerStartTag = "";
            foreach (var field in _settingsType.GetFields())
            {
                if (typeof(T) == field.FieldType)
                {
                    innerStartTag = field.Name;
                }
            }

            using (var sr = new StringReader(_optionProviderPath))
            using (var xmlReader = XmlReader.Create(sr))
            {
                xmlReader.ReadToDescendant(innerStartTag);
                var xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(innerStartTag));
                result = new Option<T>((T)xmlSerializer.Deserialize(xmlReader.ReadSubtree()));
                return result;
            }
       
        }
    }
}
