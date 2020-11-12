using System.IO;

namespace ETLService
{
    public interface IExtractable
    {
        void OnCreated(object source, FileSystemEventArgs e);
        string targetАddress { get; }
        string sourceAddress { get; }
    }
}
