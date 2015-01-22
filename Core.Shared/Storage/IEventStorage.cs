using Core.Shared.Domain;
using Core.Shared.Domain.Snapshots;
using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Storage
{
    public interface IEventStorage
    {
        IEnumerable<Event> GetEvents(Guid aggregateId);
        void Save(AggregateRoot aggregate);
        T GetSnapshot<T>(Guid aggregateId) where T : SnapshotBase;
        void SaveSnapshot(SnapshotBase snapshot);
    }
}
