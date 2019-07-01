using System.IO;

namespace FileWatcher
{
    public class ObservedFile
    {
        public ObservedFile(FileInfo fileInfo)
        {
            Info = fileInfo;
        }

        public FileInfo Info { get; }
    }
}