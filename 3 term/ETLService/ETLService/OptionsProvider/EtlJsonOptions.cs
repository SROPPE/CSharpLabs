using ETLService.Option;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Utilities;

namespace ETLService.OptionProvider
{
    public class EtlJsonOptions : OptionsProvider
    {
        public EtlJsonOptions(Type settingsType, string xmlProviderPath) : base(settingsType, xmlProviderPath)
        {

        }
        public override Option<T> GetOption<T>()
        {
            Option<T> result;
            JObject root;
            using (StreamReader sr = new StreamReader(_optionProviderPath))
            {
                root = JObject.Parse(sr.ReadToEnd());
            }

            foreach (var field in _settingsType.GetFields())
            {
                if (typeof(T) == field.FieldType)
                {
                    if (root[field.Name] != null)
                    {
                        var obj = root[field.Name].ToObject<T>();
                        result = new Option<T>(obj);
                        return result;
                    }
                }
            }

            result = new Option<T>(default);
            return result;
        }
    }
}
