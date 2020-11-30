using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Option
{
    public class ExtractionOptions
    {
        public string TargetPath { get; set; } = @"D:\Target\";
        public string SourcePath { get; set; } = @"D:\Source\";

    }
}
