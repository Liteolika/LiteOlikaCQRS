using Core.Shared.Domain.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Storage.Snapshots
{
    public interface IOriginator
    {
        SnapshotBase GetSnapshot();
        void SetSnapshot(SnapshotBase snapshot);
    }
}
