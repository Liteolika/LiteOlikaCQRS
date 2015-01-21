using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Shared.Storage;
using Core.Shared.Events;
using Core.Shared.Domain;
using Core.Shared.Domain.Mementos;
using Core.Shared.Messaging;
using Core.Shared.Exceptions;
using Core.Shared.Storage.Memento;
using Core.Shared.Utils;

namespace Core.Storage.Providers.EntityFramework
{
    public class EventStorage : IEventStorage
    {

        private readonly IEventBus _eventBus;
        private readonly EventStoreContext _storeContext;

        public EventStorage(IEventBus eventBus, EventStoreContext context)
        {
            if (eventBus == null) throw new ArgumentNullException("eventBus");
            if (context == null) throw new ArgumentNullException("context");
            _eventBus = eventBus;
            _storeContext = context;
        }

        public IEnumerable<Event> GetEvents(Guid aggregateId)
        {
            var events = _storeContext.Events.Where(p => p.AggregateId == aggregateId).Select(p => p);
            if (events.Count() == 0)
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            }
            return events;
        }

        public void Save(AggregateRoot aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            foreach (var @event in uncommittedChanges)
            {
                version++;
                if (version > 2)
                {
                    if (version % 3 == 0)
                    {
                        var originator = (IOriginator)aggregate;
                        var memento = originator.GetMemento();
                        memento.Version = version;
                        SaveMemento(memento);
                    }
                }
                @event.Version = version;
                _storeContext.Events.Add(@event);
                _storeContext.SaveChanges();
            }

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = EventConverters.ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }

        }

        public T GetMemento<T>(Guid aggregateId) where T : BaseMemento
        {
            var memento = _storeContext.Mementos.Where(m => m.Id == aggregateId).Select(m => m).LastOrDefault();
            if (memento != null)
                return (T)memento;
            return null;
        }

        public void SaveMemento(BaseMemento memento)
        {
            _storeContext.Mementos.Add(memento);
            _storeContext.SaveChanges();
        }
    }
}
