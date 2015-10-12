using System.Collections.Generic;
using System.Windows.Forms;
using Threads.Domain;

namespace Threads.Services
{
    public abstract class BaseEntryHanbler
    {
        private readonly Queue<Entry> _queue;
        private bool _working;

        protected BaseEntryHanbler()
        {
            _queue = new Queue<Entry>();
        }

        public void AddEntryToQueue(Entry entry)
        {
            lock(_queue)
            {
                _queue.Enqueue(entry);
            }
        }

        public void Write()
        {
            _working = true;
            while (_working)
            {
                if(_queue.Count == 0)
                    continue;
                lock (_queue)
                {
                    WriteEntry(_queue.Dequeue());
                }
            }
        }

        protected abstract void WriteEntry(Entry entry);
        

        public void Stop()
        {
            _working = false;
        }
    }
}
