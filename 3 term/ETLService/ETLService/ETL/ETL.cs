using System;
using System.IO;
using System.Threading;
using Utilities;

namespace ETLService
{
    public class ETL
    {
        private bool _enabled = false;
        private FileSystemWatcher _watcher;

        private static IExtractable _extractionStage;
        public static ETL Create(IExtractable extractionStage)
        {
            _extractionStage = extractionStage;

            return new ETL();
        }
        private ETL()
        {
            try
            {
                _watcher = new FileSystemWatcher();

                _watcher.Path = _extractionStage.sourceAddress;

                _watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                _watcher.Filter = "*.txt";

                _watcher.Created += _extractionStage.OnCreated;
            }
            catch(Exception exc)
            {
                Logger logger = new Logger("C:\\logger.txt", true);
                logger.Log(exc.Message);
            }
        }
        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            _enabled = true;
            while (_enabled) Thread.Sleep(100);
        }

        public void Stop()
        {
            if (_enabled)
            {
                _watcher.EnableRaisingEvents = false;
                _enabled = false;
            }
        }
    }
}
