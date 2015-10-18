using System.Collections.Generic;
using Threads.Domain;

namespace Threads.Services
{
    public abstract class BaseEntryService
    {
        private readonly Queue<Entry> _queue;
        private bool _working;
        protected Entry CurrentEntry { get; private set; }

        protected BaseEntryService()
        {
            _queue = new Queue<Entry>();
        }

        public void AddEntryToQueue(Entry entry)
        {
            lock (_queue)
            {
                _queue.Enqueue(entry);
            }
        }

        public void Write()
        {
            _working = true;
            while (_working)
            {
                if (_queue.Count == 0)
                    continue;
                lock (_queue)
                {
                    CurrentEntry = _queue.Dequeue();
                    WriteEntry();
                }
            }
        }

        protected abstract void WriteEntry();


        public void Stop()
        {
            _working = false;
        }
    }
}
