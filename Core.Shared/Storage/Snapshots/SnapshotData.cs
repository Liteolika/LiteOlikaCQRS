using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Storage.Snapshots
{
    public class SnapshotData
    {

        protected SnapshotData()
        { }

        public SnapshotData(Guid aggregateId, int version, byte[] eventData)
        {
            this.MementoId = Guid.NewGuid();
            this.AggregateId = aggregateId;
            this.Version = version;
            this.Data = eventData;
        }

        [Key]
        public Guid MementoId { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public byte[] Data { get; set; }

    }
}
