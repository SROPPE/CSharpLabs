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
    }
}
