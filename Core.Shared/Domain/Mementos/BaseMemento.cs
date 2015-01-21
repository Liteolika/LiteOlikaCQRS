using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Domain.Mementos
{
    [Serializable]
    public class BaseMemento
    {
        public Guid Id { get; protected set; }
        public int Version { get; set; }
    }
}
