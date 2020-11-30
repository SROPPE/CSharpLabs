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
        public EtlXmlOptions(string xmlProviderPath) : base(xmlProviderPath)
        {

        }
        public override Option<T> GetOption<T>()
        {
            EtlOptions options = new EtlOptions();
            Option<T> result = new Option<T>(default);

            XmlSerializer formatter = new XmlSerializer(typeof(EtlOptions));
            try
            {
                using (FileStream fs = new FileStream("config.xml", FileMode.OpenOrCreate))
                {
                     options = (EtlOptions)formatter.Deserialize(fs);
                }
            }
            catch (Exception exc)
            {
                Logger.Log(exc.Message);
            }
            foreach (var field in options.GetType().GetFields())
            {
                if(typeof(T) == field.GetType())
                {
                    
                }
            }
          
            return result;
        }
    }
}
