using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.EntityFramework
{
    public class DbContextBase : DbContext
    {

        public DbContextBase(string connectionStringOrName)
            : base(connectionStringOrName)
        {

        }

    }
}
