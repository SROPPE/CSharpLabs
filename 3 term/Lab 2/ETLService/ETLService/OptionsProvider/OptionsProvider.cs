using ETLService.Option;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.OptionProvider
{
    public abstract class OptionsProvider
    {
        protected Type _settingsType;
        protected string _optionProviderPath;
        public abstract Option<T> GetOption<T>();
        public OptionsProvider(Type settingsType,string optionProviderPath)
        {
            _optionProviderPath = optionProviderPath;
            _settingsType = settingsType;
        }
    }
}
