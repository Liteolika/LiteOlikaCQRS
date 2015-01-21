using Core.Shared.Domain.Mementos;
using Core.Shared.Events;
using Core.Shared.Storage.Memento;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.EntityFramework
{
    public static class Serializers
    {

        public static EventData EventSerializer(Event @event)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, @event);
                EventData e = new EventData(@event.AggregateId, @event.aggregateVersion, ms.ToArray());
                return e;
            }
        }

        public static Event EventDeserializer(EventData eventData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(eventData.Data, 0, eventData.Data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                Event @event = (Event)bf.Deserialize(ms);
                return @event;
            }
        }

        public static MementoData MementoSerializer(BaseMemento memento)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, memento);
                MementoData e = new MementoData(memento.Id, 
                    memento.Version, 
                    ms.ToArray());
                return e;
            }
        }

        public static T MementoDeserializer<T>(MementoData mementoData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(mementoData.Data, 0, mementoData.Data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                T memento = (T)bf.Deserialize(ms);
                return memento;
            }
        }


    }
}
