using System;
using System.IO;

namespace FileWatcher
{
    public class SimpleIntervalFileWatcher : IntervalFileWatcher<ObservedFile>
    {
        public SimpleIntervalFileWatcher(DirectoryInfo observedDirectory, string searchPattern = "*") : base(
            observedDirectory,
            searchPattern) { }

        public SimpleIntervalFileWatcher(string observedDirectoryPath, string searchPattern = "*") : base(
            observedDirectoryPath,
            searchPattern) { }

        public SimpleIntervalFileWatcher(DirectoryInfo observedDirectory, string searchPattern = "*",
            bool recursive = false) : base(observedDirectory, searchPattern, recursive) { }

        public SimpleIntervalFileWatcher(string observedDirectoryPath, TimeSpan pollingInterval,
            string searchPattern = "*", bool recursive = false) : base(
            observedDirectoryPath,
            pollingInterval,
            searchPattern,
            recursive) { }

        public SimpleIntervalFileWatcher(DirectoryInfo observedDirectory, TimeSpan pollingInterval,
            string searchPattern = "*", bool recursive = false) : base(
            observedDirectory,
            pollingInterval,
            searchPattern,
            recursive) { }
    }
}