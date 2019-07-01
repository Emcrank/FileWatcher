using System;

namespace FileWatcher
{
    public interface IFileWatcher<out T>
    {
        IDisposable Subscribe(Action<T> onNext);

        void Start();

        void Stop();
    }
}