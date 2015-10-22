using System.Collections.Generic;
using System.Threading;
using Threads.Domain;

namespace Threads.Services
{
    public abstract class BaseEntryService
    {
        private readonly Queue<Entry> _queue;
        public volatile bool Working;
        protected Entry CurrentEntry { get; private set; }
        private readonly object _syncObj = new object();

        protected BaseEntryService()
        {
            _queue = new Queue<Entry>();
        }

        public void AddEntryToQueue(Entry entry)
        {
            lock (_syncObj)
            {
                _queue.Enqueue(entry);
                Monitor.Pulse(_syncObj);
            }
        }

        public void Write()
        {
            Working = true;
            Monitor.Enter(_syncObj);
            while (Working)
            {
                lock (_syncObj)
                {
                    if (_queue.Count == 0)
                        Monitor.Wait(_syncObj);

                    CurrentEntry = _queue.Dequeue();
                    WriteEntry();
                }
            }

            Monitor.Exit(_syncObj);
        }

        protected abstract void WriteEntry();

        public void Stop()
        {
            Working = false;
        }
    }
}
