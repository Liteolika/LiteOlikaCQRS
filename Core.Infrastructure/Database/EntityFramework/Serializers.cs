using Core.Shared.Domain.Snapshots;
using Core.Shared.Events;
using Core.Shared.Storage.Snapshots;
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

        public static SnapshotData SnapshotSerializer(SnapshotBase snapshot)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, snapshot);
                SnapshotData e = new SnapshotData(snapshot.Id, 
                    snapshot.Version, 
                    ms.ToArray());
                return e;
            }
        }

        public static T SnapshotDeserializer<T>(SnapshotData snapshotData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(snapshotData.Data, 0, snapshotData.Data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                T snapshot = (T)bf.Deserialize(ms);
                return snapshot;
            }
        }


    }
}
