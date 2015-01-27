using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Storage
{
    public class EventData
    {
        protected EventData()
        { }

        public EventData(Guid aggregateId, int aggregateVersion, byte[] eventData)
        {
            this.EventId = Guid.NewGuid();
            this.AggregateId = aggregateId;
            this.AggregateVersion = aggregateVersion;
            this.Data = eventData;
        }

        [Key]
        public Guid EventId { get; set; }
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        
        public byte[] Data { get; set; }

    }
}
