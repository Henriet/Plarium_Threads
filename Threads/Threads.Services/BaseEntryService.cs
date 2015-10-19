using System.Collections.Generic;
using System.Windows.Forms;
using Threads.Domain;

namespace Threads.Services
{
    public abstract class BaseEntryService
    {
        private readonly Queue<Entry> _queue;
        public bool Working { get; private set; }
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
            Working = true;
            while (Working)
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
            Working = false;
        }
    }
}
