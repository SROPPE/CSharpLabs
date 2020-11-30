using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Option
{
    public class EtlOptions
    {
        public ArchiveOptions archiveOptions = new ArchiveOptions();
        public ExtractionOptions sourceOptions = new ExtractionOptions();
        public LoggerOptions loggerOptions = new LoggerOptions();
        private static EtlOptions _instance;
        public static EtlOptions GetInstance()
        {
            if (_instance == null)
            {
                return new EtlOptions();
            }
            return _instance;
        }
        private EtlOptions()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
    }
}
