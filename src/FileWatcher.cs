using System;

namespace FileWatcher
{
    public abstract class FileWatcher<T> : IFileWatcher<T> where T : ObservedFile
    {
        protected IObservable<T> Files;

        public FileWatcherStatus Status { get; protected set; }

        public IDisposable Subscribe(Action<T> onNext)
        {
            if (Status == FileWatcherStatus.Idle)
                throw new InvalidOperationException("You cannot subscribe to the watcher while it is in Idle state.");

            return Files.Subscribe(onNext);
        }

        public virtual void Start()
        {
            Status = FileWatcherStatus.Watching;
        }

        public virtual void Stop()
        {
            Status = FileWatcherStatus.Idle;
        }
    }
}