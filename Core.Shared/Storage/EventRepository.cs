using Core.Shared.Domain;
using Core.Shared.Domain.Mementos;
using Core.Shared.Events;
using Core.Shared.Exceptions;
using Core.Shared.Storage.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Storage
{
    public class EventRepository<T> : IEventRepository<T> 
        where T : AggregateRoot, new()
    {

        private readonly IEventStorage _storage;

        public EventRepository(IEventStorage storage)
        {
            if (storage == null) throw new ArgumentNullException("storage");
            _storage = storage;

        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            if (aggregate.GetUncommittedChanges().Any())
            {
                var item = new T();
                if (expectedVersion != -1)
                {
                    item = GetById(aggregate.AggregateId);
                    if (item.AggregateVersion != expectedVersion)
                        throw new ConcurrencyException(
                            string.Format("Aggregate {0} has been previously modified", item.AggregateId));
                }
                _storage.Save(aggregate);
            }
        }

        public T GetById(Guid id)
        {
            IEnumerable<Event> events;
            var memento = _storage.GetMemento<BaseMemento>(id);
            if (memento != null)
            {
                events = _storage.GetEvents(id).Where(e => e.aggregateVersion >= memento.Version);
            }
            else
            {
                events = _storage.GetEvents(id);
            }
            var obj = new T();
            if (memento != null)
                ((IOriginator)obj).SetMemento(memento);

            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}
