using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Commands
{
    [Serializable]
    public class Command : ICommand
    {
        public Guid AggregateId { get; private set; }
        public int AggregateVersion { get; private set; }

        public Command(Guid aggregateId, int aggregateVersion)
        {
            this.AggregateId = aggregateId;
            this.AggregateVersion = aggregateVersion;
        }
    }
}
