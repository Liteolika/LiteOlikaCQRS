using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared.ViewModels
{
    public interface IViewModelRepository<TViewModel>
    {
        TViewModel GetById(Guid id);
        void Add(TViewModel item);
        void Delete(Guid id);
        List<TViewModel> GetItems();
    }
}
