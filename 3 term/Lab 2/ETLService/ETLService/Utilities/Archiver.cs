using System;
using System.IO;
using System.IO.Compression;
using ETLService.Option;
namespace Utilities
{
    public static class Archiver
    {
        public static ArchiveOptions options = new ArchiveOptions();
        public static string CompressFile(PathWrapper currentFilePath)
        {
            string createdFile = null;

            FileInfo fileToCompress = new FileInfo(currentFilePath.FullPath);
            using (FileStream originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) &
                   FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(
                        Path.Combine(currentFilePath.FileDirectory, currentFilePath.FileName) + ".gz"))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, options.compressionLevel))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            createdFile = compressedFileStream.Name;
                        }
                    }

                }
            }
            return createdFile;
        }
        public static string DecompressFile(PathWrapper currentFilePath)
        {
            if (currentFilePath.Extension != ".gz")
            {
                throw new ArgumentException("Wrong file format.");
            }

            FileInfo fileToDecompress = new FileInfo(currentFilePath.FullPath);
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
            return Path.Combine(currentFilePath.FileDirectory, currentFilePath.FileName);
        }
    }
}
