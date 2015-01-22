using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Domain.Snapshots
{
    [Serializable]
    public class SnapshotBase
    {
        public Guid Id { get; protected set; }
        public int Version { get; set; }
    }
}
