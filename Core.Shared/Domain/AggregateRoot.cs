using Core.Shared.Events;
using Core.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Domain
{
    public abstract class AggregateRoot : IEventProvider
    {
        private readonly List<Event> _changes;

        public Guid AggregateId { get; protected set; }
        public int AggregateVersion { get; set; }
        //public int EventVersion { get; set; }

        protected AggregateRoot()
        {
            _changes = new List<Event>();
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
            AggregateVersion = history.Last().aggregateVersion;
            //EventVersion = AggregateVersion;
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            dynamic d = this;

            var e = EventConverters.ChangeTo(@event, @event.GetType());
            d.Handle(e);
            if (isNew)
            {
                _changes.Add(@event);
            }
        }
    }
}
