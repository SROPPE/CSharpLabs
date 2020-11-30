using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
namespace ETLService.Option
{
    public class ArchiveOptions
    {
        public CompressionLevel compressionLevel { get; set; } = CompressionLevel.Optimal;
    }
}
