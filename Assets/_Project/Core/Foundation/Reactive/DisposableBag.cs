using System;
using System.Collections.Generic;

namespace Core.Foundation.Reactive
{
    public class DisposableBag : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();
        private bool _isDisposed = false;

        public void Add(IDisposable disposable)
        {
            if (disposable == null) return;

            if (_isDisposed)
            {
                disposable.Dispose();
                return;
            }

            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            int count = _disposables.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    _disposables[i]?.Dispose();
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Error disposing item: {exception}");
                }
            }

            _disposables.Clear();
        }
    }
}
