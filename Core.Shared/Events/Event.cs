using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Events
{
    [Serializable]
    public class Event // : IEvent
    {
        //public Event()
        //{
        //    this.EventId = Guid.NewGuid();
        //}

        //[Key]
        //public Guid EventId { get; private set; }
        public Guid AggregateId { get; set; }
        public int aggregateVersion { get; set; }

        //public string EventType { get; set; }
        //public byte[] EventData { get; set; }
    }
}
