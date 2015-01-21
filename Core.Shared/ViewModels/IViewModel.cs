using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.ViewModels
{
    public class IViewModel
    {
        Guid Id { get; set; }
        int Version { get; set; }
    }
}
