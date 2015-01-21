using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.Commands
{
    public interface ICommand
    {
        Guid AggregateId { get; }
    }
}
