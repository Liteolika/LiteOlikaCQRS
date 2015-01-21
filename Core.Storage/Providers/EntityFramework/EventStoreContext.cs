using Core.Shared.Domain.Mementos;
using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage.Providers.EntityFramework
{
    public class EventStoreContext : BaseContext
    {

        public EventStoreContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<BaseMemento> Mementos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
