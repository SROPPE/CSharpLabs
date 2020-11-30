using ETLService.Option;
using System;
using System.IO;
using System.Threading;
using Utilities;

namespace ETLService.Extraction
{
    public class ExtractionStage : IExtractable
    {

        private ExtractionOptions _options;
        public ExtractionOptions Options 
        { 
            get => _options; 
            set => _options = value;
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            Thread.Sleep(100);

            try
            {
                FileInfo file = new FileInfo(e.FullPath);

                string fileDirectory = CreateDirectory(file.LastWriteTime);
                string newPath = PutCreatedFileInFolder(file, fileDirectory);

                byte[] key = AesEncryption.GenerateRandomKey(16);

                EncryptFile(newPath, key);

                var compressedFilePath = Archiver.CompressFile(new PathWrapper(newPath));

                string newCompressedFilePath = _options.TargetPath + Path.GetFileName(compressedFilePath);
                File.Move(compressedFilePath, newCompressedFilePath);

                string decompressedFilePath = Archiver.DecompressFile(new PathWrapper(newCompressedFilePath));

                DecryptFile(decompressedFilePath, key);
            }
            catch(Exception exc)
            {
                Logger.Log(exc.Message);
            }
        }

        private void EncryptFile(string newPath, byte[] key)
        {         
                string data;
                using (StreamReader sr = new StreamReader(newPath))
                {
                    data = sr.ReadToEnd();
                }
                string encryptData = AesEncryption.Encrypt(data, key);
                using (StreamWriter sw = new StreamWriter(newPath))
                {
                    sw.Write(encryptData);
                }
            
        
        }

        private void DecryptFile(string newPath, byte[] key)
        {
            string data;
            using (StreamReader sr = new StreamReader(newPath))
            {
                data = sr.ReadToEnd();
            }
            string decryptData = AesEncryption.Decrypt(data, key);
            using (StreamWriter sw = new StreamWriter(newPath))
            {
                sw.Write(decryptData);
            }
        }

        private string PutCreatedFileInFolder(FileInfo file, string fileDirectory)
        {
            string newName = CreateUnicName(file, fileDirectory);
            PathWrapper path = new PathWrapper(file.FullName);

            var newPath = Path.Combine(fileDirectory,
                                       PathWrapper.CreateValidFileName(() => newName)
                                       + path.Extension);
            File.Move(file.FullName, newPath);

            return newPath;
        }
        private string CreateUnicName(FileInfo file, string td)
        {
            string newName = $"{Path.GetFileNameWithoutExtension(file.FullName)}_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";

            int i = 0;

            while (File.Exists(Path.Combine(td, newName) + file.Extension))
            {
                i++;
                newName = $"{Path.GetFileNameWithoutExtension(file.FullName)}_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}({i})";
            }
            return newName;
        }
        private string CreateDirectory(DateTime date)
        {
            string td = $"{_options.SourcePath}{date:yyyy\\\\MM\\\\dd}";
            if (!Directory.Exists(td))
            {
                Directory.CreateDirectory(td);
            }

            return td;
        }
    }
}
