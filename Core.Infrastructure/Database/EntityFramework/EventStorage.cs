using Core.Shared.Domain;
using Core.Shared.Domain.Mementos;
using Core.Shared.Events;
using Core.Shared.Exceptions;
using Core.Shared.Messaging;
using Core.Shared.Storage;
using Core.Shared.Storage.Memento;
using Core.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.EntityFramework
{
    public class EventStorage : DbContextBase, IEventStorage
    {

        private readonly IEventBus _eventBus;

        public DbSet<EventData> Events { get; set; }
        public DbSet<MementoData> Mementos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public EventStorage(IEventBus eventBus, string connectionStringOrName)
            : base(connectionStringOrName)
        {
            if (eventBus == null) throw new ArgumentNullException("eventBus");
            _eventBus = eventBus;
        }

        public IEnumerable<Event> GetEvents(Guid aggregateId)
        {

            var dbEvents = Events
                .Where(x => x.AggregateId == aggregateId)
                .Select(x => x)
                .OrderBy(x => x.AggregateVersion);

            List<Event> events = new List<Event>();
            foreach (var e in dbEvents)
            {
                var ev = Serializers.EventDeserializer(e);
                events.Add(ev);
            }

            if (events.Count() == 0)
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            
            return events;
        }

        public void Save(AggregateRoot aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.AggregateVersion;

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
                @event.aggregateVersion = version;

                var eventData = Serializers.EventSerializer(@event);

                Events.Add(eventData);
                SaveChanges();
            }
            foreach (var @event in uncommittedChanges)
            {
                var desEvent = EventConverters.ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }



        public T GetMemento<T>(Guid aggregateId) where T : BaseMemento
        {

            var me = Mementos.Where(x => x.AggregateId == aggregateId).ToList();
            if (me != null)
            {
                var last = me.Select(x => x).LastOrDefault();
                if (last != null)
                    return Serializers.MementoDeserializer<T>(last);
            }
            return null;
        }

        public void SaveMemento(BaseMemento memento)
        {
            var mementoData = Serializers.MementoSerializer(memento);
            Mementos.Add(mementoData);
            SaveChanges();
        }

        


       

    }
}
