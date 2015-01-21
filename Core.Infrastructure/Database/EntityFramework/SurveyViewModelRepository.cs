using Core.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.EntityFramework
{
    public class SurveyViewModelRepository<T> : IViewModelRepository<T>
        where T : ViewModelBase
    {

        private DbSet<T> _set;

        public SurveyViewModelRepository(DbSet<T> set)
        {
            if (set == null) throw new ArgumentNullException("store");
            this._set = set;
        }

        public T GetById(Guid id)
        {
            return _set.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(T item)
        {
            _set.Add(item);
        }

        public void Delete(Guid id)
        {
            var items = _set.Where(x => x.Id == id).ToList();
            _set.RemoveRange(items);
        }

        public List<T> GetItems()
        {
            return _set.ToList();
        }
    }
}
