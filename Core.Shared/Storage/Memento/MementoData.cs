using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Storage.Memento
{
    public class MementoData
    {

        protected MementoData()
        { }

        public MementoData(Guid aggregateId, int version, byte[] eventData)
        {
            this.MementoId = Guid.NewGuid();
            this.AggregateId = aggregateId;
            this.Version = version;
            this.Data = eventData;
        }

        [Key]
        public Guid MementoId { get; private set; }
        public Guid AggregateId { get; set; }
        public int Version;

        public byte[] Data { get; set; }

    }
}
