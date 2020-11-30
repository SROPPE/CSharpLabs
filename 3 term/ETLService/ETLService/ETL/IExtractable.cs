using ETLService.Option;
using System.IO;

namespace ETLService
{
    public interface IExtractable
    {
        void OnCreated(object source, FileSystemEventArgs e);
        ExtractionOptions Options { get; set; }
    }
}
