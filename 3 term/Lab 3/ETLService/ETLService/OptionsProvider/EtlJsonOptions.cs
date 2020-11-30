using ETLService.Option;
using Newtonsoft.Json;
using System;
using System.IO;
using Utilities;

namespace ETLService.OptionProvider
{
    public class EtlJsonOptions : OptionsProvider
    {
        public EtlJsonOptions(string jsonProviderPath) : base(jsonProviderPath)
        {

        }
        public override Option<T> GetOption<T>()
        {
            Option<T> result;


            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader(_optionProviderPath))
            {
                using (JsonTextReader textReader = new JsonTextReader(sr))
                {
                    var deserialized = serializer.Deserialize<T>(textReader);
                    result = new Option<T>(deserialized);
                }
            }
            return result;
        }
    }
}
