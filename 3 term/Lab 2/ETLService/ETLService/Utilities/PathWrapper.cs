using System;
using System.IO;

namespace Utilities
{
    public class PathWrapper
    {
        private string _fileDirectory;
        private string _fileName;
        private string _fullPath;
        private string _fileExtension;

        public string FileDirectory => _fileDirectory;
        public string FileName => _fileName;
        public string FullPath => _fullPath;
        public string Extension => _fileExtension;

        public PathWrapper(string path)
        {
            if (IsFullPathCorrect(path))
            {
                SetPathParts(path);
            }
            else throw new ArgumentException();
        }

        public void SetPathParts(string path)
        {
            _fullPath = Path.GetFullPath(path);
            _fileDirectory = Path.GetDirectoryName(path);
            _fileName = Path.GetFileNameWithoutExtension(path);
            _fileExtension = Path.GetExtension(path);
        }

        public static string CreateValidPath(Func<string> getPath)
        {
            if (getPath == null) throw new ArgumentNullException();

            string path;
            do
            {
                path = getPath.Invoke();
            } while (!IsPathCorrect(path));

            return path;
        }
        public static string CreateValidFileName(Func<string> getFileName)
        {
            if (getFileName == null) throw new ArgumentNullException();
           
            string fileName;
            do
            {
                fileName = getFileName.Invoke();
            } while (!IsFileNameCorrect(fileName));

            return fileName;
        }
        public static string CreateValidFullPath(Func<string> getFullPath)
        {
            if (getFullPath == null) throw new ArgumentNullException();

            string fullPath;
            do
            {
                fullPath = getFullPath.Invoke();
            } while (!IsFullPathCorrect(fullPath));

            return fullPath;
        }

        public static bool IsPathCorrect(string path)
        {
            var invalidChars = Path.GetInvalidPathChars();

            foreach (var symbol in invalidChars)
            {
                
                if (path.Contains(symbol.ToString()))
                    return false;
            }
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                return false;
            }
            return true;
        }
        public static bool IsFileNameCorrect(string path)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var fileName = Path.GetFileName(path);
            foreach (var symbol in invalidChars)
            {
                if (fileName.Contains(symbol.ToString()))
                {
                    return false;
                }
            }
            if (string.IsNullOrEmpty(Path.GetFileName(path))) return false;

            return true;
        }
        public static bool IsFullPathCorrect(string path)
        {

            if (string.IsNullOrWhiteSpace(path)) return false;

            if (IsPathCorrect(path) && IsFileNameCorrect(path)) return true;

            return false;
        }
    }
}
