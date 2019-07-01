using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace FileWatcher
{
    public class IntervalFileWatcher<T> : FileWatcher<T> where T : ObservedFile
    {
        private readonly DirectoryInfo observedDirectory;
        private readonly TimeSpan pollingInterval = TimeSpan.FromSeconds(60);
        private readonly SearchOption searchOption;
        private readonly string searchPattern;

        public IntervalFileWatcher(DirectoryInfo observedDirectory, string searchPattern = "*")
        {
            if (observedDirectory == null)
                throw new ArgumentNullException(nameof(observedDirectory));

            if (!observedDirectory.Exists)
                throw new DirectoryNotFoundException(
                    $"The directory given '{observedDirectory.FullName}' to observe does not exist or cannot be accessed.");

            if (string.IsNullOrWhiteSpace(searchPattern))
                throw new ArgumentException("The search pattern cannot be null or whitespace.");

            this.observedDirectory = observedDirectory;
            this.searchPattern = searchPattern;
        }

        public IntervalFileWatcher(string observedDirectoryPath, string searchPattern = "*")
            : this(new DirectoryInfo(observedDirectoryPath), searchPattern) { }

        public IntervalFileWatcher(string observedDirectoryPath, TimeSpan pollingInterval, string searchPattern = "*",
            bool recursive = false)
            : this(new DirectoryInfo(observedDirectoryPath), pollingInterval, searchPattern, recursive) { }

        public IntervalFileWatcher(DirectoryInfo observedDirectory, string searchPattern = "*", bool recursive = false)
            : this(observedDirectory, searchPattern)
        {
            searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        }

        public IntervalFileWatcher(DirectoryInfo observedDirectory, TimeSpan pollingInterval,
            string searchPattern = "*",
            bool recursive = false)
            : this(observedDirectory, searchPattern)
        {
            if (pollingInterval == TimeSpan.Zero)
                throw new ArgumentException("You cannot set a polling interval of Zero.");

            this.pollingInterval = pollingInterval;
            searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        }

        public override void Start()
        {
            base.Start();

            Files = Observable.Interval(pollingInterval)
                .Select(
                    x => observedDirectory.EnumerateFiles(searchPattern, searchOption)
                        .Select(f => new ObservedFile(f))
                        .Cast<T>()
                        .ToObservable()
                ).SelectMany(x => x)
                .TakeWhile(x => Status == FileWatcherStatus.Watching);
        }
    }
}